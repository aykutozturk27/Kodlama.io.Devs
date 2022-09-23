using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : UserForRegisterDto, IRequest<AccessToken>
    {
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            public RegisterUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules, IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
                _operationClaimRepository = operationClaimRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<AccessToken> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserEmailCanNotBeDuplicatedWhenInserted(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    Status = true,
                    AuthenticatorType = AuthenticatorType.Email
                };

                OperationClaim? claim = await _operationClaimRepository.GetAsync(p => p.Name == "User");

                User newUser = await _userRepository.AddAsync(user);

                UserOperationClaim userOperationClaim = new UserOperationClaim
                {
                    UserId = newUser.Id,
                    OperationClaimId = claim.Id,
                };

                await _userOperationClaimRepository.AddAsync(userOperationClaim);

                var claims = await _userOperationClaimRepository.GetListAsync(
                    p => p.UserId == newUser.Id,
                    include: p => p.Include(c => c.OperationClaim),
                    cancellationToken: cancellationToken);

                var accessToken = _tokenHelper.CreateToken(newUser, claims.Items.Select(p => p.OperationClaim).ToList());
                return accessToken;
            }
        }
    }
}
