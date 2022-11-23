using MediatR;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.SaveFile
{
    public class SaveFileCommandHandler : IRequestHandler<SaveFileCommand, string>
    {
        private readonly IFileRepository _filesRepository;

        public SaveFileCommandHandler(IFileRepository filesRepository)
        {
            _filesRepository = filesRepository;
        }

        public async Task<string> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _filesRepository.SaveFileAsync(request.FilePath, request.FileName, request.FileContent);

            return file.Hash;
        }
    }
}