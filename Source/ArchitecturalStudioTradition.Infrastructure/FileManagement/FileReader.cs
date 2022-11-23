using ArchitecturalStudioTradition.Infrastructure.FileManagement.Models;
using Microsoft.AspNetCore.Http;

namespace ArchitecturalStudioTradition.Infrastructure.FileManagement
{
    public static class FileReader
    {
        public static async IAsyncEnumerable<FileInformation> ReadAsync(IFormFileCollection files)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files), "Provided file is null.");

            foreach (IFormFile file in files)
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                yield return new FileInformation(file.FileName, stream, file.ContentType);
            }
        }
    }
}
