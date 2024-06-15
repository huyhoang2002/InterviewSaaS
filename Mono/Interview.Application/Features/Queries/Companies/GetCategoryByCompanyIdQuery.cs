using AutoMapper;
using Interview.Application.DTO.QueryDTO;
using Interview.Infrastructure.CQRS.Queries;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Queries.Companies
{
    public class GetCategoryByCompanyIdQuery : IQuery<IEnumerable<JobCategoryDTO>>
    {
        public Guid CompanyId { get; set; }
    }

    public class GetCategoryByCompanyIdQueryHandler : IQueryHandler<GetCategoryByCompanyIdQuery, IEnumerable<JobCategoryDTO>>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoryByCompanyIdQueryHandler(ICompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<JobCategoryDTO>> Handle(GetCategoryByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var company = _repository.FindOneById(_ => _.Id == request.CompanyId, cancellationToken);
            if (company is null)
            {
                return Task.FromResult(new List<JobCategoryDTO>() as IEnumerable<JobCategoryDTO>);
            }
            var jobCategories = company.GetJobCategories(request.CompanyId);
            var jobCategoryDTO = jobCategories.Select(_ => new JobCategoryDTO(_.Id, _.CategoryName));
            return Task.FromResult(jobCategoryDTO);
        }
    }
}
