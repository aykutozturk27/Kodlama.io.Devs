using Application.Features.ProgrammingTechnologies.Constants;
using FluentValidation;

namespace Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology
{
    public class CreateProgrammingTechnologyCommandValidator : AbstractValidator<CreateProgrammingTechnologyCommand>
    {
        public CreateProgrammingTechnologyCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(Messages.ProgrammingTechnologyNameIsNotEmpty);
        }
    }
}
