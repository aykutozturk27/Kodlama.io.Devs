using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();

            CreateMap<User, LoginCommand>().ReverseMap();
            CreateMap<AccessToken, UserForLoginDto>().ReverseMap();
            CreateMap<User, UserForLoginDto>().ReverseMap();
        }
    }
}
