using Amazon.S3.Model;
using Amazon.S3;
using ArchitecturalStudioTradition.FileStorage.Application.Configuration;
using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Extensions;

namespace ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Helpers
{
    internal interface IS3RequestBuilder
    {
        GetObjectRequest CreateGetRequest(string objectPath, string? objectName = null);
        ListObjectsRequest CreateListRequest(string objectPath);
        PutObjectRequest CreatePutRequest(string objectPath, string objectName, Stream stream);
        DeleteObjectRequest CreateDeleteRequest(string objectPath, string? objectName = null);
    }

    internal class S3RequestBuilder : IS3RequestBuilder
    {
        private readonly IAwsConfiguration _configuration;

        public S3RequestBuilder(IAwsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GetObjectRequest CreateGetRequest(string objectPath, string? objectName = null)
        {
            return new GetObjectRequest
            {
                Key = string.IsNullOrEmpty(objectName) ? objectPath : $"{objectPath}/{objectName}",
                BucketName = _configuration.AwsBucketName,
            };
        }

        public ListObjectsRequest CreateListRequest(string objectPath)
        {
            return new ListObjectsRequest
            {
                Prefix = $"{objectPath}/",
                BucketName = _configuration.AwsBucketName
            };
        }

        public PutObjectRequest CreatePutRequest(string objectPath, string objectName, Stream stream)
        {
            return new PutObjectRequest
            {
                Key = objectName,
                FilePath = objectPath,
                BucketName = _configuration.AwsBucketName,
                StorageClass = S3StorageClass.Standard,
                CannedACL = S3CannedACL.Private,
                InputStream = stream,
                MD5Digest = stream.Md5Hash().ToBase64String()
            };
        }

        public DeleteObjectRequest CreateDeleteRequest(string objectPath, string? objectName = null)
        {
            return new DeleteObjectRequest
            {
                Key = string.IsNullOrEmpty(objectName) ? objectPath : $"{objectPath}/{objectName}",
                BucketName = _configuration.AwsBucketName
            };
        }
    }
}
