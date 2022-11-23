using ArchitecturalStudioTradition.CQRS.Interfaces;
using MediatR;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.SaveFile
{
    public class SaveFileCommand : ICommand<string>, IQuery<bool>, IQuery<Unit>
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}