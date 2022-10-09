using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? userToCheck = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);

                _authBusinessRules.UserShouldExistWhenRequested(userToCheck);

                var verifyPassword = HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt);
                _authBusinessRules.UserPasswordWrongWhenRequested(verifyPassword);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(userToCheck);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(userToCheck, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoginedDto loginDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken
                };
                return loginDto;
            }
        }
    }
}
