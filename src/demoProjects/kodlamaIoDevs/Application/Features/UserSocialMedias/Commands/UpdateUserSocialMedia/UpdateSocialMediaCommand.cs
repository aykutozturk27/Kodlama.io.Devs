using Application.Features.UserSocialMedias.Dtos;
using Application.Features.UserSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserSocialMedias.Commands.UpdateUserSocialMedia
{
    public class UpdateUserSocialMediaCommand : IRequest<UpdatedUserSocialMediaDto>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public class UpdateUserSocialMediaCommandHandler : IRequestHandler<UpdateUserSocialMediaCommand, UpdatedUserSocialMediaDto>
        {
            private readonly IUserSocialMediaRepository _userSocialMediaRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaBusinessRules _userSocialMediaBusinessRules;

            public UpdateUserSocialMediaCommandHandler(IUserSocialMediaRepository userSocialMediaRepository, IMapper mapper, UserSocialMediaBusinessRules userSocialMediaBusinessRules)
            {
                _userSocialMediaRepository = userSocialMediaRepository;
                _mapper = mapper;
                _userSocialMediaBusinessRules = userSocialMediaBusinessRules;
            }

            public async Task<UpdatedUserSocialMediaDto> Handle(UpdateUserSocialMediaCommand request, CancellationToken cancellationToken)
            {
                UserSocialMedia? userSocialMedia = await _userSocialMediaRepository.GetAsync(p => p.Id == request.Id);

                await _userSocialMediaBusinessRules.UserSocialMediaUrlCanNotBeDuplicatedWhenUpdated(request.Url);

                userSocialMedia.Url = request.Url;

                UserSocialMedia updatedUserSocialMedia = await _userSocialMediaRepository.UpdateAsync(userSocialMedia);
                UpdatedUserSocialMediaDto updatedUserSocialMediaDto = _mapper.Map<UpdatedUserSocialMediaDto>(updatedUserSocialMedia);

                return updatedUserSocialMediaDto;
            }
        }
    }
}
