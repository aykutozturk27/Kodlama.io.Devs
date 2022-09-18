using Application.Features.UserSocialMedias.Dtos;
using Application.Features.UserSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserSocialMedias.Commands.CreateUserSocialMedia
{
    public class CreateUserSocialMediaCommand : IRequest<CreatedUserSocialMediaDto>
    {
        public int UserId { get; set; }
        public string Url { get; set; }
        public class CreateUserSocialMediaCommandHandler : IRequestHandler<CreateUserSocialMediaCommand, CreatedUserSocialMediaDto>
        {
            private readonly IUserSocialMediaRepository _userSocialMediaRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaBusinessRules _userSocialMediaBusinessRules;

            public CreateUserSocialMediaCommandHandler(IUserSocialMediaRepository userSocialMediaRepository, IMapper mapper, UserSocialMediaBusinessRules userSocialMediaBusinessRules)
            {
                _userSocialMediaRepository = userSocialMediaRepository;
                _mapper = mapper;
                _userSocialMediaBusinessRules = userSocialMediaBusinessRules;
            }

            public async Task<CreatedUserSocialMediaDto> Handle(CreateUserSocialMediaCommand request, CancellationToken cancellationToken)
            {
                await _userSocialMediaBusinessRules.UserSocialMediaUrlCanNotBeDuplicatedWhenInserted(request.Url);
                await _userSocialMediaBusinessRules.UserShouldExistWhenRequested(request.UserId);

                UserSocialMedia mappedUserSocialMedia = _mapper.Map<UserSocialMedia>(request);
                UserSocialMedia createdUserSocialMedia = await _userSocialMediaRepository.AddAsync(mappedUserSocialMedia);
                CreatedUserSocialMediaDto createdUserSocialMediaDto = _mapper.Map<CreatedUserSocialMediaDto>(createdUserSocialMedia);

                return createdUserSocialMediaDto;
            }
        }
    }
}
