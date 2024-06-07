using FluentValidation;
using Interview.Application.Features.Commands.User;
using Interview.Domain.Aggregates.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Behaviors.Validators
{
    public class AddUserValidator : AbstractValidator<User>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
        }
    }
}
