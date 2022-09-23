using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.RegisterUser;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();

            CreateMap<User, LoginUserCommand>().ReverseMap();
            CreateMap<AccessToken, UserForLoginDto>().ReverseMap();
            CreateMap<User, UserForLoginDto>().ReverseMap();
        }
    }
}
