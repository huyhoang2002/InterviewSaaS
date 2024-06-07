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

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CommandResult<Guid>> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            var user = new Interview.Domain.Aggregates.User.User(
                request.FirstName,
                request.LastName,
                request.Age,
                request.Gender,
                request.PhoneNumber,
                request.Address,
                request.City,
                request.Province,
                request.CitizenId
            );
            var validation = new AddUserValidator().Validate(user);
            if (validation.IsValid)
            {
                _userRepository.Add(user);
                return CommandResult<Guid>.Success(user.Id);
            }
            else
            {
                return CommandResult<Guid>.Error("Validation failed");
            }
        }
    }
}
