using Application.Features.Users.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void UserShouldExistWhenRequested(User user)
        {
            if (user == null) throw new BusinessException(Messages.UserShouldExistWhenRequested);
        }

        public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(b => b.Email == email);
            if (result.Items.Any()) throw new BusinessException(Messages.UserEmailCanNotBeDuplicatedWhenInserted);
        }

        public void UserVerifyPasswordHashWhenInserted(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
                throw new BusinessException(Messages.PasswordError);
        }
    }
}
