using MediatR;
using System.Collections.Immutable;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFileList
{
    public class GetFileListQueryHandler : IRequestHandler<GetFileListQuery, ImmutableList<(string fileName, Uri fileUrl)>>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileListQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<ImmutableList<(string fileName, Uri fileUrl)>> Handle(GetFileListQuery request, CancellationToken cancellationToken)
        {
            var files = _fileRepository.GetFileListAsync(request.Hash);

            return (await files.ToListAsync(cancellationToken: cancellationToken))
                    .Select(file => (file.FileName, file.FileUrl))
                    .ToImmutableList();
        }
    }
}