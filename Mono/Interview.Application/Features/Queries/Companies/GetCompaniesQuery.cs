using Interview.Application.DTO.QueryDTO;
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
    public class GetCompaniesQuery : IQuery<IEnumerable<CompanyDTO>>
    {
    }

    public class GetCompaniesQueryHandler : IQueryHandler<GetCompaniesQuery, IEnumerable<CompanyDTO>>
    {
        private readonly ICompanyRepository _repository;

        public GetCompaniesQueryHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<CompanyDTO>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = _repository.GetCompanies();
            var companiesViewModel = companies.Select(_ => new CompanyDTO(
                    _.Id,
                    _.CompanyName,
                    _.CompanyLogoUrl,
                    _.CompanyDescription,
                    _.CompanyDomain,
                    _.CompanyPhoneNumber
                ));
            return Task.FromResult(companiesViewModel);
        }
    }
}
