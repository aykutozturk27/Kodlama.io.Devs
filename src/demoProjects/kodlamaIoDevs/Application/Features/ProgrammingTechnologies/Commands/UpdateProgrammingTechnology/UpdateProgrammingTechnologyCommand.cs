using Application.Features.ProgrammingTechnologies.Dtos;
using Application.Features.ProgrammingTechnologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology
{
    public class UpdateProgrammingTechnologyCommand : IRequest<UpdatedProgrammingTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateProgrammingTechnologyCommandHandler : IRequestHandler<UpdateProgrammingTechnologyCommand, UpdatedProgrammingTechnologyDto>
        {
            private readonly IProgrammingTechnologyRepository _programmingTechnologyRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingTechnologyBusinessRules _programmingTechnologyBusinessRules;

            public UpdateProgrammingTechnologyCommandHandler(IProgrammingTechnologyRepository programmingTechnologyRepository, IMapper mapper, ProgrammingTechnologyBusinessRules programmingTechnologyBusinessRules)
            {
                _programmingTechnologyRepository = programmingTechnologyRepository;
                _mapper = mapper;
                _programmingTechnologyBusinessRules = programmingTechnologyBusinessRules;
            }

            public async Task<UpdatedProgrammingTechnologyDto> Handle(UpdateProgrammingTechnologyCommand request, CancellationToken cancellationToken)
            {
                ProgrammingTechnology? programmingTechnology = await _programmingTechnologyRepository.GetAsync(p => p.Id == request.Id);

                await _programmingTechnologyBusinessRules.ProgrammingTechnologyNameCanNotBeDuplicatedWhenUpdated(request.Name);

                programmingTechnology.Name = request.Name;

                ProgrammingTechnology updatedProgrammingTechnology = await _programmingTechnologyRepository.UpdateAsync(programmingTechnology);
                UpdatedProgrammingTechnologyDto updatedProgrammingTechnologyDto = _mapper.Map<UpdatedProgrammingTechnologyDto>(updatedProgrammingTechnology);

                return updatedProgrammingTechnologyDto;
            }
        }
    }
}
