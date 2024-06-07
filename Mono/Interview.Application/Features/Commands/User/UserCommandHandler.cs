using AutoMapper;
using Interview.Application.Behaviors.Validators;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.User
{
    public class UserCommand : ICommand<CommandResult<Guid>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? Province { get; set; }
        public string CitizenId { get; set; }
    }

    public class UserCommandHandler : ICommandHandler<UserCommand, CommandResult<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<CommandResult<Guid>> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            var userMapper = _mapper.Map<Interview.Domain.Aggregates.User.User>(request);
            var validation = new AddUserValidator().Validate(userMapper);
            if (validation.IsValid)
            {
                _userRepository.Add(userMapper);
                return Task.FromResult(CommandResult<Guid>.Success(userMapper.Id));
            }
            else
            {
                return Task.FromResult(CommandResult<Guid>.Error("Validation failed"));
            }
        }
    }
}
