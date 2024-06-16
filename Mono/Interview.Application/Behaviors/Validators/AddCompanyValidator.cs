using FluentValidation;
using Interview.Domain.Aggregates.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Behaviors.Validators
{
    public class AddCompanyValidator : AbstractValidator<Company>
    {
        public AddCompanyValidator()
        {
            RuleFor(_ => _.CompanyName).NotNull().NotEmpty();
            RuleFor(_ => _.CompanyDomain).NotEmpty().NotEmpty();
            RuleFor(_ => _.Email).NotEmpty().EmailAddress();
        }
    }
}
