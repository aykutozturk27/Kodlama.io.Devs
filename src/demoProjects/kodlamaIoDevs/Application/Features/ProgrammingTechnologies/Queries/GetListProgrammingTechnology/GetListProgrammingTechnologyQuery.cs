using Application.Features.ProgrammingTechnologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology
{
    public class GetListProgrammingTechnologyQuery : IRequest<ProgrammingTechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListProgrammingTechnologyQueryHandler : IRequestHandler<GetListProgrammingTechnologyQuery, ProgrammingTechnologyListModel>
        {

            private readonly IMapper _mapper;
            private readonly IProgrammingTechnologyRepository _programmingTechnologyRepository;

            public GetListProgrammingTechnologyQueryHandler(IMapper mapper, IProgrammingTechnologyRepository programmingTechnologyRepository)
            {
                _mapper = mapper;
                _programmingTechnologyRepository = programmingTechnologyRepository;
            }

            public async Task<ProgrammingTechnologyListModel> Handle(GetListProgrammingTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingTechnology> programmingTechnologies = await _programmingTechnologyRepository.GetListAsync(include:
                                              m => m.Include(c => c.ProgrammingLanguage),
                                              index: request.PageRequest.Page,
                                              size: request.PageRequest.PageSize
                                              );

                ProgrammingTechnologyListModel mappedProgrammingTechnologies = _mapper.Map<ProgrammingTechnologyListModel>(programmingTechnologies);
                return mappedProgrammingTechnologies;
            }
        }
    }
}
