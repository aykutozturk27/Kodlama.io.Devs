using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IUserSocialMediaRepository : IAsyncRepository<UserSocialMedia>, IRepository<UserSocialMedia>
    {
    }
}
