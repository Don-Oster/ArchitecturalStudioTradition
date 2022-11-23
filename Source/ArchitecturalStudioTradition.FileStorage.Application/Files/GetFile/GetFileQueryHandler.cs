using ArchitecturalStudioTradition.FileStorage.Contract.v1;
using MediatR;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, GetFileResponse>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<GetFileResponse> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileAsync(request.Hash);

            return new GetFileResponse { 
                File = new FileModel { FileName = file.FileName, FileUrl = file.FileUrl } 
            };
        }
    }
}