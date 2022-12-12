using MediatR;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, (string fileName, Uri fileUrl)>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<(string fileName, Uri fileUrl)> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileAsync(request.Hash);

            return (file.FileName, file.FileUrl);
        }
    }
}