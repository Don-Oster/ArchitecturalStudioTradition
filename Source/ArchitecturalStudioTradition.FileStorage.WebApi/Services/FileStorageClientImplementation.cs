using ArchitecturalStudioTradition.CQRS;
using ArchitecturalStudioTradition.FileStorage.Application.Files.GetFile;
using ArchitecturalStudioTradition.FileStorage.Application.Files.GetFileList;
using ArchitecturalStudioTradition.FileStorage.Application.Files.SaveFile;
using ArchitecturalStudioTradition.FileStorage.Client;
using Grpc.Core;
using static ArchitecturalStudioTradition.FileStorage.Client.FileStorage;

namespace ArchitecturalStudioTradition.FileStorage.WebApi.Services
{
    public class FileStorageClientImplementation : FileStorageBase
    {
        private readonly ICommandBus _commandBus;

        public FileStorageClientImplementation(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public override async Task<GetFileResponse> GetFile(GetFileRequest request, ServerCallContext context)
        {
            var query = new GetFileQuery
            {
                Hash = request.Hash
            };

            var (fileName, fileUrl) = await _commandBus.SendAsync(query);

            return new GetFileResponse
            {
                File = new FileModel { FileName = fileName, FileUrl = fileUrl.ToString() }
            }
            ;
        }

        public override async Task<GetFileListResponse> GetFileList(GetFileListRequest request, ServerCallContext context)
        {
            var query = new GetFileListQuery
            {
                Hash = request.Hash
            };

            var result = await _commandBus.SendAsync(query);

            return new GetFileListResponse
            {
                Files = { result.Select(file => new FileModel {
                    FileName = file.fileName,
                    FileUrl = file.fileUrl.ToString() }) 
                }
            }
           ;
        }

        public override async Task<SaveFileResponse> SaveFile(SaveFileRequest request, ServerCallContext context)
        {
            var query = new SaveFileCommand
            {
                FilePath = request.FilePath,
                FileName = request.FileName,
                FileContent = request.FileContent.ToArray()
            };

            var result = await _commandBus.SendAsync(query);

            return new SaveFileResponse { Hash = result };
        }
    }
}