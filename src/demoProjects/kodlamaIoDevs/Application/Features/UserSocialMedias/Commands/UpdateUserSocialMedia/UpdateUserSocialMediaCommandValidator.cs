using Application.Features.UserSocialMedias.Constants;
using FluentValidation;

namespace Application.Features.UserSocialMedias.Commands.UpdateUserSocialMedia
{
    public class UpdateUserSocialMediaCommandValidator : AbstractValidator<UpdateUserSocialMediaCommand>
    {
        public UpdateUserSocialMediaCommandValidator()
        {
            RuleFor(c => c.Url).NotEmpty().WithMessage(Messages.UserSocialMediaUrlIsNotEmpty);
        }
    }
}
