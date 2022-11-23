using ArchitecturalStudioTradition.FileStorage.Contract.v1;
using MediatR;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFileList
{
    public class GetFileListQueryHandler : IRequestHandler<GetFileListQuery, GetFileListResponse>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileListQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<GetFileListResponse> Handle(GetFileListQuery request, CancellationToken cancellationToken)
        {
            var files = _fileRepository.GetFileListAsync(request.Hash);

            return new GetFileListResponse() {
                Files = (await files.ToListAsync())
                    .Select(file => new FileModel { FileName = file.FileName, FileUrl = file.FileUrl })
                    .ToList()
            };
        }
    }
}