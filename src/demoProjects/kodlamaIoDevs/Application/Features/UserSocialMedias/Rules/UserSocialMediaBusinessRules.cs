using Application.Features.UserSocialMedias.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.UserSocialMedias.Rules
{
    public class UserSocialMediaBusinessRules
    {
        private readonly IUserSocialMediaRepository _userSocialMediaRepository;

        public UserSocialMediaBusinessRules(IUserSocialMediaRepository userSocialMediaRepository)
        {
            _userSocialMediaRepository = userSocialMediaRepository;
        }

        public async Task UserSocialMediaUrlCanNotBeDuplicatedWhenInserted(string url)
        {
            IPaginate<UserSocialMedia> result = await _userSocialMediaRepository.GetListAsync(b => b.Url == url);
            if (result.Items.Any()) throw new BusinessException(Messages.UserSocialMediaUrlCanNotBeDuplicatedWhenInserted);
        }

        public async Task UserSocialMediaUrlCanNotBeDuplicatedWhenUpdated(string url)
        {
            IPaginate<UserSocialMedia> result = await _userSocialMediaRepository.GetListAsync(b => b.Url == url);
            if (result.Items.Any()) throw new BusinessException(Messages.UserSocialMediaUrlCanNotBeDuplicatedWhenUpdated);
        }

        public void UserSocialMediaShouldExistWhenRequested(UserSocialMedia userSocialMedia)
        {
            if (userSocialMedia == null) throw new BusinessException(Messages.UserSocialMediaShouldExistWhenRequested);
        }

        public async Task UserShouldExistWhenRequested(int userId)
        {
            IPaginate<UserSocialMedia> result = await _userSocialMediaRepository.GetListAsync(b => b.UserId == userId);
            if (!result.Items.Any()) throw new BusinessException(Messages.UserSocialMediaShouldExistWhenRequested);
        }
    }
}
