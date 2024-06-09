using FluentValidation;
using Interview.Domain.Aggregates.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Behaviors.Validators
{
    public class RegisterValidator : AbstractValidator<Account>
    {
        public RegisterValidator() 
        {
            RuleFor(_ => _.Email).NotEmpty().EmailAddress();
            RuleFor(_ => _.UserName).NotEmpty();
        }
    }
}
