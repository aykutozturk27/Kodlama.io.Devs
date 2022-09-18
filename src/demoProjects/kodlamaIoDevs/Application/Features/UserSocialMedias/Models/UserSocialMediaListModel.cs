using Application.Features.UserSocialMedias.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.UserSocialMedias.Models
{
    public class UserSocialMediaListModel : BasePageableModel
    {
        public IList<UserSocialMediaListDto> Items { get; set; }
    }
}
