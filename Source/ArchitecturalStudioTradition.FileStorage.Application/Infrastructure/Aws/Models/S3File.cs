namespace ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Models
{
    public class S3File
    {
        public string Hash { get; set; }
        public string FileName { get; set; }
        public Uri FileUrl { get; set; }
    }
}
