using Application.Features.ProgrammingTechnologies.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingTechnologies.Rules
{
    public class ProgrammingTechnologyBusinessRules
    {
        private IProgrammingTechnologyRepository _programmingTechnologyRepository;

        public ProgrammingTechnologyBusinessRules(IProgrammingTechnologyRepository programmingTechnologyRepository)
        {
            _programmingTechnologyRepository = programmingTechnologyRepository;
        }

        public async Task ProgrammingTechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingTechnology> result = await _programmingTechnologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException(Messages.ProgrammingTechnologyNameCanNotBeDuplicatedWhenInserted);
        }

        public async Task ProgrammingTechnologyNameCanNotBeDuplicatedWhenUpdated(string name)
        {
            IPaginate<ProgrammingTechnology> result = await _programmingTechnologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException(Messages.ProgrammingTechnologyNameCanNotBeDuplicatedWhenUpdated);
        }

        public void ProgrammingTechnologyShouldExistWhenRequested(ProgrammingTechnology programmingTechnology)
        {
            if (programmingTechnology == null) throw new BusinessException(Messages.ProgrammingTechnologyShouldExistWhenRequested);
        }

        public async Task ProgrammingLanguageShouldExistWhenRequested(int programmingLanguageId)
        {
            IPaginate<ProgrammingTechnology> result = await _programmingTechnologyRepository.GetListAsync(b => b.ProgrammingLanguageId == programmingLanguageId);
            if (!result.Items.Any()) throw new BusinessException(Messages.ProgrammingLanguageShouldExistWhenRequested);
        }
    }
}
