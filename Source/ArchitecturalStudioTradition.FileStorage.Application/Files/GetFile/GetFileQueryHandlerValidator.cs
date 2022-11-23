using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.GetFile
{
    public class GetFileQueryHandlerValidator : AbstractValidator<GetFileQuery>
    {
        public GetFileQueryHandlerValidator()
        {
            RuleFor(x => x.Hash).NotEmpty().WithMessage(RequestValidationMessages.HashNotEmpty);
        }
    }
}