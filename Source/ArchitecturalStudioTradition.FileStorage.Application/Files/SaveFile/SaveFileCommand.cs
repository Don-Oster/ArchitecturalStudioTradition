using ArchitecturalStudioTradition.CQRS.Interfaces;
using MediatR;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.SaveFile
{
    public record SaveFileCommand : ICommand<string>, IQuery<bool>, IQuery<Unit>
    {
        public required string FilePath { get; init; }
        public required string FileName { get; init; }
        public required byte[] FileContent { get; init; }
    }
}