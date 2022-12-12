using ArchitecturalStudioTradition.CQRS.Interfaces;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFile
{
    public record GetFileQuery : ICommand<(string fileName, Uri fileUrl)>
    {
        public required string Hash { get; init; }
    }
}