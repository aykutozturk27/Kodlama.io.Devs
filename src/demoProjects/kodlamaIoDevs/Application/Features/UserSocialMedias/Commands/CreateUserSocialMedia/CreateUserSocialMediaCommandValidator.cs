using Application.Features.UserSocialMedias.Constants;
using FluentValidation;

namespace Application.Features.UserSocialMedias.Commands.CreateUserSocialMedia
{
    public class CreateUserSocialMediaCommandValidator : AbstractValidator<CreateUserSocialMediaCommand>
    {
        public CreateUserSocialMediaCommandValidator()
        {
            RuleFor(c => c.Url).NotEmpty().WithMessage(Messages.UserSocialMediaUrlIsNotEmpty);
        }
    }
}
