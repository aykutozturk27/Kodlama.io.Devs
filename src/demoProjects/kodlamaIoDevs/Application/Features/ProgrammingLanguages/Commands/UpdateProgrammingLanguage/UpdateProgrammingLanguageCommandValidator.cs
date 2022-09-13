using Application.Features.ProgrammingLanguages.Constants;
using FluentValidation;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommandValidator : AbstractValidator<UpdateProgrammingLanguageCommand>
    {
        public UpdateProgrammingLanguageCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(Messages.ProgrammingLanguageNameIsNotEmpty);
        }
    }
}
