using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Companies
{
    public class DeleteCompanyCommand : ICommand<CommandResult<Guid>>
    {
        public Guid CompanyId { get; set; }
    }

    public class DeleteCompanyCommandHandler : ICommandHandler<DeleteCompanyCommand, CommandResult<Guid>>
    {
        private readonly ICompanyRepository _repository;

        public DeleteCompanyCommandHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult<Guid>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = _repository.FindOneById(_ => _.Id == request.CompanyId, cancellationToken);
            if (company is null)
            {
                return CommandResult<Guid>.Error("Company is not found");
            }
            company.DisableCompany();
            return CommandResult<Guid>.Success(company.Id);
        }
    }
}
