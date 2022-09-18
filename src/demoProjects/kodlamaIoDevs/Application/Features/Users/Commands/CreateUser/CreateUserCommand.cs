using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserForRegisterDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserForRegisterDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UserForRegisterDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserEmailCanNotBeDuplicatedWhenInserted(request.Email);

                User mappedUser = _mapper.Map<User>(request);
                User createdUser = await _userRepository.AddAsync(mappedUser);
                UserForRegisterDto userForRegisterDto = _mapper.Map<UserForRegisterDto>(createdUser);
                return userForRegisterDto;
            }
        }
    }
}
