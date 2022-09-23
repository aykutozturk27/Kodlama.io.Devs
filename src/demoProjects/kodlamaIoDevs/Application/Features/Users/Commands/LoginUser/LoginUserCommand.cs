using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : UserForLoginDto, IRequest<AccessToken>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? userToCheck = await _userRepository.GetAsync(u => u.Email == request.Email);

                _userBusinessRules.UserShouldExistWhenRequested(userToCheck);

                var claims = await _userOperationClaimRepository.GetListAsync(uc => uc.UserId == userToCheck.Id,
                    include: u => u.Include(c => c.OperationClaim),
                    cancellationToken: cancellationToken);

                AccessToken accessToken = _tokenHelper.CreateToken(userToCheck, claims.Items.Select(p => p.OperationClaim).ToList());
                return accessToken;
            }
        }
    }
}
