using Amazon.S3.Model;
using Amazon.S3;
using PgcrDashboards.Admin.Web.Infrastructure;
using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Models;
using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Extensions;
using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Helpers;

namespace ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws
{
    public interface IS3FileRepository
    {
        IAsyncEnumerable<S3File> GetFileListAsync(string hash);
        Task<S3File> GetFileAsync(string hash);
        Task DeleteFileAsync(string hash);
        Task<S3File> SaveFileAsync(string filePath, string fileName, byte[] fileContent);
    }

    internal class S3FileRepository : IS3FileRepository
    {
        private readonly IS3Client _s3Client;
        private readonly IS3RequestBuilder _requestBuilder;
        private readonly ICloudFrontUrlProvider _cloudFrontUrlProvider;

        public S3FileRepository(IS3Client s3Client, IS3RequestBuilder requestBuilder, ICloudFrontUrlProvider cloudFrontUrlProvider)
        {
            _s3Client = s3Client;
            _requestBuilder = requestBuilder;
            _cloudFrontUrlProvider = cloudFrontUrlProvider;
        }

        public async IAsyncEnumerable<S3File> GetFileListAsync(string hash)
        {
            var request = _requestBuilder.CreateListRequest(hash);

            ListObjectsResponse response;
            do
            {
                response = await _s3Client.PerformOperationAsync(client => client.ListObjectsAsync(request), "fetching files");

                foreach (var s3Object in response.S3Objects)
                {
                    yield return new S3File
                    {
                        FileUrl = _cloudFrontUrlProvider.GetCloudFrontUrl(s3Object.Key),
                        FileName = Path.GetFileName(s3Object.Key),
                        Hash = s3Object.Key
                    };
                }

                request.Marker = response.NextMarker;
            } while (response.IsTruncated);
        }

        public async Task<S3File> GetFileAsync(string hash)
        {
            var request = _requestBuilder.CreateGetRequest(hash);
            var response = await _s3Client.PerformOperationAsync(client => client.GetObjectAsync(request), "fetching file");

            return new S3File
            {
                FileUrl = _cloudFrontUrlProvider.GetCloudFrontUrl(response.Key),
                FileName = Path.GetFileName(response.Key),
                Hash = response.Key
            };
        }

        public async Task DeleteFileAsync(string hash)
        {
            var request = _requestBuilder.CreateDeleteRequest(hash);

            await _s3Client.PerformOperationAsync(client => client.DeleteObjectAsync(request), "deleting file");
        }

        public async Task<S3File> SaveFileAsync(string filePath, string fileName, byte[] fileContent)
        {
            using var fileStream = new MemoryStream(fileContent);
            var request = _requestBuilder.CreatePutRequest(filePath, fileName, fileStream);

            var response = await _s3Client.PerformOperationAsync(client => client.PutObjectAsync(request), "uploading file");

            CheckETagFromResponse(request, response);

            return new S3File
            {
                FileUrl = _cloudFrontUrlProvider.GetCloudFrontUrl(request.Key),
                FileName = fileName,
                Hash = request.Key
            };
        }

        #region Private Methods

        private static void CheckETagFromResponse(PutObjectRequest request, PutObjectResponse response)
        {
            var actualETag = response.ETag.ToS3ETagString();
            var expectedETag = request.MD5Digest.FromBase64String().ToS3ETagString();
            if (actualETag != expectedETag)
            {
                throw new AmazonS3Exception("The eTag received from S3 doesn't match the eTag computed before uploading. This usually indicates that the file attachment has been corrupted in transit.");
            }
        }

        #endregion
    }
}
