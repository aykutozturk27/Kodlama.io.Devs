using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserSocialMediaRepository : EfRepositoryBase<UserSocialMedia, BaseDbContext>, IUserSocialMediaRepository
    {
        public UserSocialMediaRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
