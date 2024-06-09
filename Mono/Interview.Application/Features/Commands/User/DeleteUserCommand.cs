using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Interview.Application.Features.Commands.User
{
    public class DeleteUserCommand : ICommand<CommandResult<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, CommandResult<Guid>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<CommandResult<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.FindOneById(user => user.Id == request.Id, cancellationToken);
            if (user == null)
            {
                return Task.FromResult(CommandResult<Guid>.Error("No user found !"));
            }
            var result = user.SoftDelete();
            if (result == true)
            {
                return Task.FromResult(CommandResult<Guid>.Success(user.Id));
            }
            else
            {
                return Task.FromResult(CommandResult<Guid>.Error("Failed to remove user !"));
            }
        }
    }
}
