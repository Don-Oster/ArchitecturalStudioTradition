using ArchitecturalStudioTradition.CQRS.Interfaces;
using System.Collections.Immutable;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFileList
{
    public record GetFileListQuery : ICommand<ImmutableList<(string fileName, Uri fileUrl)>>
    {
        public required string Hash { get; init; }
    }
}