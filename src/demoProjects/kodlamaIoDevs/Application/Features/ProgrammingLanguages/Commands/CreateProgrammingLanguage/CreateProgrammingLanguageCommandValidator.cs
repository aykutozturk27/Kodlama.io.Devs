﻿using Application.Features.ProgrammingLanguages.Constants;
using FluentValidation;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommandValidator : AbstractValidator<CreateProgrammingLanguageCommand>
    {
        public CreateProgrammingLanguageCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(Messages.ProgrammingLanguageNameIsNotEmpty);
        }
    }
}
