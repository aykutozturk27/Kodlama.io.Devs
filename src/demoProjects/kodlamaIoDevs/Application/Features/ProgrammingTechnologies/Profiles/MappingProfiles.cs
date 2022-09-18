using Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Dtos;
using Application.Features.ProgrammingTechnologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingTechnologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgrammingTechnology, ProgrammingTechnologyListDto>()
                .ForMember(c => c.ProgrammingLanguageName,
                opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();
            CreateMap<IPaginate<ProgrammingTechnology>, ProgrammingTechnologyListModel>().ReverseMap();
            CreateMap<ProgrammingTechnology, CreateProgrammingTechnologyCommand>().ReverseMap();
            CreateMap<ProgrammingTechnology, CreatedProgrammingTechnologyDto>().ReverseMap();
            CreateMap<ProgrammingTechnology, UpdatedProgrammingTechnologyDto>().ReverseMap();
            CreateMap<ProgrammingTechnology, DeletedProgrammingTechnologyDto>().ReverseMap();
        }
    }
}
