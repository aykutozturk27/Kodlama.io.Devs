using Application.Features.UserSocialMedias.Dtos;
using Application.Features.UserSocialMedias.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.UserSocialMedias.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserSocialMedia, UserSocialMediaListDto>()
                .ForMember(c => c.UserEmail,
                opt => opt.MapFrom(c => c.User.Email))
                .ReverseMap();
            CreateMap<IPaginate<UserSocialMedia>, UserSocialMediaListModel>().ReverseMap();
            CreateMap<UserSocialMedia, CreatedUserSocialMediaDto>().ReverseMap();
            CreateMap<UserSocialMedia, UpdatedUserSocialMediaDto>().ReverseMap();
            CreateMap<UserSocialMedia, DeletedUserSocialMediaDto>().ReverseMap();
        }
    }
}
