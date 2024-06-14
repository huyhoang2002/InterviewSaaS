using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Companies
{
    public class AddJobCategoryCommand : ICommand<CommandResult<Guid>>
    {
        public Guid CompanyId { get; set; }
        public string CategoryName { get; set; }
    }

    public class AddJobCategoryCommandHandler : ICommandHandler<AddJobCategoryCommand, CommandResult<Guid>>
    {
        private readonly ICompanyRepository _repository;

        public AddJobCategoryCommandHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult<Guid>> Handle(AddJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var company = _repository.FindOneById(_ => _.Id == request.CompanyId, cancellationToken); 
            
            if (company is null)
            {
                return CommandResult<Guid>.Error("No company found !");
            }
            if (company.IsJobCategoryCreated(request.CategoryName)) 
            {
                return CommandResult<Guid>.Error("This job has been created !");
            }
            company.CreateJobCategory(request.CategoryName);
            return CommandResult<Guid>.Success(company.Id);
        }
    }
}
