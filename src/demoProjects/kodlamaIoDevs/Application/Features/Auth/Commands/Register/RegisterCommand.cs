using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : UserForRegisterDto, IRequest<AccessToken>
    {
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public RegisterCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                User? userToCheck = await _userRepository.GetAsync(u => u.Email == request.Email);

                _userBusinessRules.UserShouldExistWhenRequested(userToCheck);
                _userBusinessRules.UserVerifyPasswordHashWhenInserted(request.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt);

                var claims = await _userRepository.GetClaims(userToCheck);
                AccessToken accessToken = _tokenHelper.CreateToken(userToCheck, claims);
                return accessToken;
            }
        }
    }
}
