using AutoMapper;
using FluentValidation;
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
    public class UpdateUserCommand : ICommand<CommandResult<Guid>>
    {
        public Guid UserId { get; set; }
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

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, CommandResult<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Interview.Domain.Aggregates.User.User> _validator;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<Interview.Domain.Aggregates.User.User> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CommandResult<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.FindOneById(user => user.Id == request.UserId);
            var userMapper = _mapper.Map<Interview.Domain.Aggregates.User.User>(request);
            var validator = _validator.Validate(userMapper);
            if (validator.IsValid)
            {
                user.UpdateUser(userMapper);
                return CommandResult<Guid>.Success(user.Id);
            }
            else
            {
                return CommandResult<Guid>.Error("Update validation failed !");
            }
        }
    }
}
