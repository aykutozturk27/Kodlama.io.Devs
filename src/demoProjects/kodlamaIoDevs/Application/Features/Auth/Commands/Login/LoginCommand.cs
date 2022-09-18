using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand : UserForLoginDto, IRequest<AccessToken>
    {
        public string Email { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? userToCheck = await _userRepository.GetAsync(u => u.Email == request.Email);

                _userBusinessRules.UserShouldExistWhenRequested(userToCheck);

                var claims = await _userRepository.GetClaims(userToCheck);
                AccessToken accessToken = _tokenHelper.CreateToken(userToCheck, claims);
                return accessToken;
            }
        }
    }
}
