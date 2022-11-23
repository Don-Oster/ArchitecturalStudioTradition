using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.FileStorage.Contract.v1;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFileList
{
    public class GetFileListQuery : ICommand<GetFileListResponse>
    {
        public string Hash { get; set; }
    }
}