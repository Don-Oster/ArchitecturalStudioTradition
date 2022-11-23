using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.FileStorage.Application.Files.SaveFile
{
    public class SaveFileCommandHandlerValidator : AbstractValidator<SaveFileCommand>
    {
        public SaveFileCommandHandlerValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage(RequestValidationMessages.FileNameNotEmpty);
            RuleFor(x => x.FileContent).NotEmpty().WithMessage(RequestValidationMessages.FileContentNotEmpty);
        }
    }
}