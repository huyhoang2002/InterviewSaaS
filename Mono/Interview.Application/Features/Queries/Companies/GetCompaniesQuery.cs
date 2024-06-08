using Interview.Domain.Companies;
using Interview.Infrastructure.CQRS.Queries;
using Interview.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Queries.Companies
{
    public class GetCompaniesQuery : IQuery<IEnumerable<Company>>
    {
    }

    public class GetCompaniesQueryHandler : IQueryHandler<GetCompaniesQuery, IEnumerable<Company>>
    {
        private readonly ICompanyRepository _repository;

        public GetCompaniesQueryHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Company>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = _repository.GetAll();
            return Task.FromResult(companies);
        }
    }
}
