using Application.Features.UserSocialMedias.Dtos;
using Application.Features.UserSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserSocialMedias.Commands.DeleteUserSocialMedia
{
    public class DeleteUserSocialMediaCommand : IRequest<DeletedUserSocialMediaDto>
    {
        public int Id { get; set; }
        public class DeleteUserSocialMediaCommandHandler : IRequestHandler<DeleteUserSocialMediaCommand, DeletedUserSocialMediaDto>
        {
            private readonly IUserSocialMediaRepository _userSocialMediaRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaBusinessRules _userSocialMediaBusinessRules;

            public DeleteUserSocialMediaCommandHandler(IUserSocialMediaRepository userSocialMediaRepository, IMapper mapper, 
                UserSocialMediaBusinessRules userSocialMediaBusinessRules)
            {
                _userSocialMediaRepository = userSocialMediaRepository;
                _mapper = mapper;
                _userSocialMediaBusinessRules = userSocialMediaBusinessRules;
            }

            public async Task<DeletedUserSocialMediaDto> Handle(DeleteUserSocialMediaCommand request, CancellationToken cancellationToken)
            {
                UserSocialMedia? userSocialMedia = await _userSocialMediaRepository.GetAsync(p => p.Id == request.Id);

                _userSocialMediaBusinessRules.UserSocialMediaShouldExistWhenRequested(userSocialMedia);

                UserSocialMedia deletedUserSocialMedia = await _userSocialMediaRepository.DeleteAsync(userSocialMedia);
                DeletedUserSocialMediaDto deletedUserSocialMediaDto = _mapper.Map<DeletedUserSocialMediaDto>(deletedUserSocialMedia);

                return deletedUserSocialMediaDto;
            }
        }
    }
}
