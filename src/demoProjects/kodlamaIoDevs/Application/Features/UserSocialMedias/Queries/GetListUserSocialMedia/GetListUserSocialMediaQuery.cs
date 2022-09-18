using Application.Features.UserSocialMedias.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserSocialMedias.Queries.GetListUserSocialMedia
{
    public class GetListUserSocialMediaQuery : IRequest<UserSocialMediaListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListUserSocialMediaQueryHandler : IRequestHandler<GetListUserSocialMediaQuery, UserSocialMediaListModel>
        {
            private readonly IMapper _mapper;
            private readonly IUserSocialMediaRepository _userSocialMediaRepository;

            public GetListUserSocialMediaQueryHandler(IMapper mapper, IUserSocialMediaRepository userSocialMediaRepository)
            {
                _mapper = mapper;
                _userSocialMediaRepository = userSocialMediaRepository;
            }

            public async Task<UserSocialMediaListModel> Handle(GetListUserSocialMediaQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserSocialMedia> userSocialMedias = await _userSocialMediaRepository.GetListAsync(
                                                                                                               include: usm => usm.Include(x => x.User),
                                                                                                               index: request.PageRequest.Page,
                                                                                                               size: request.PageRequest.PageSize
                                                                                                           );

                UserSocialMediaListModel userSocialMediaListModel = _mapper.Map<UserSocialMediaListModel>(userSocialMedias);
                return userSocialMediaListModel;
            }
        }
    }
}
