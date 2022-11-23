namespace ArchitecturalStudioTradition.Infrastructure.FileManagement.Models
{
    public class FileInformation
    {
        public string FileName { get; set; }
        public Stream FileContent { get; set; }
        public string ContentType { get; }

        public FileInformation(string fileName, Stream fileContent, string contentType)
        {
            FileName = fileName;
            FileContent = fileContent;
            ContentType = contentType;
        }
    }
}
