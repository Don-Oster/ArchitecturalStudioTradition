using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws;
using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Models;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files
{
    public interface IFileRepository
    {
        IAsyncEnumerable<S3File> GetFileListAsync(string hash);
        Task<S3File> GetFileAsync(string hash);
        Task DeleteFileAsync(string hash);
        Task<S3File> SaveFileAsync(string filePath, string fileName, byte[] fileContent);
    }

    internal class FileRepository : IFileRepository
    {
        private const string  S3FilesPath = "Images";

        private readonly IS3FileRepository _s3FileRepository;

        public FileRepository(IS3FileRepository s3FileRepository)
        {
            _s3FileRepository = s3FileRepository;
        }

        public async Task<S3File> GetFileAsync(string hash)
        {
            return await _s3FileRepository.GetFileAsync(GetS3Path(hash));
        }

        public IAsyncEnumerable<S3File> GetFileListAsync(string hash)
        {
            return _s3FileRepository.GetFileListAsync(GetS3Path(hash));
        }

        public Task DeleteFileAsync(string hash)
        {
            return _s3FileRepository.DeleteFileAsync(GetS3Path(hash));
        }

        public async Task<S3File> SaveFileAsync(string filePath, string fileName, byte[] fileContent)
        {
            return await _s3FileRepository.SaveFileAsync(GetS3Path(filePath), fileName, fileContent);
        }

        #region Private Methods

        private static string GetS3Path(string path)
        {
            return $"{S3FilesPath}/{path}";
        }

        #endregion
    }
}