using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.FileStorage.Contract.v1;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFile
{
    public class GetFileQuery : ICommand<GetFileResponse>
    {
        public string Hash { get; set; }
    }
}