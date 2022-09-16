using Application.Features.ProgrammingTechnologies.Constants;
using FluentValidation;

namespace Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology
{
    public class UpdateProgrammingTechnologyCommandValidator : AbstractValidator<UpdateProgrammingTechnologyCommand>
    {
        public UpdateProgrammingTechnologyCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(Messages.ProgrammingTechnologyNameIsNotEmpty);
        }
    }
}
