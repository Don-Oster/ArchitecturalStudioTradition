using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFileList
{
    public class GetFileListQueryHandlerValidator : AbstractValidator<GetFileListQuery>
    {
        public GetFileListQueryHandlerValidator()
        {
            RuleFor(x => x.Hash).NotEmpty().WithMessage(RequestValidationMessages.HashNotEmpty);
        }
    }
}