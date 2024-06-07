using FluentValidation;
using Interview.Domain.Aggregates.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Behaviors.Validators
{
    public class UpdateUserValidator : AbstractValidator<User>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
