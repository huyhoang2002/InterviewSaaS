using Interview.Application.Behaviors.Validators;
using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.CQRS.Commands;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Accounts
{
    public class CreateAccountCommand : ICommand<CommandResult<string>>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, CommandResult<string>>
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateAccountCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<CommandResult<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Email, request.UserName);
            var validator = new RegisterValidator().Validate(account);
            if (validator.IsValid is false)
            {
                return CommandResult<string>.Error("Failed to validate input");
            }
            var result = await _userManager.CreateAsync(account, request.Password);
            if (result.Succeeded is false)
            {
                return CommandResult<string>.Error("Failed to create account");
            }
            var isRoleExist = await _roleManager.RoleExistsAsync(Role.USER);
            if (isRoleExist == false) 
            { 
                var role = new IdentityRole(Role.USER);
                await _roleManager.CreateAsync(role);
                await _userManager.AddToRoleAsync(account, role.Name);
            }
            return CommandResult<string>.Success(account.Id);
        }
    }
}
