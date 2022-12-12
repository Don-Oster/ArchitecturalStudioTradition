namespace ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Models
{
    public record S3File
    {
        public required string Hash { get; init; }
        public required string FileName { get; init; }
        public required Uri FileUrl { get; init; }
    }
}
