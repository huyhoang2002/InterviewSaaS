using AutoMapper;
using Interview.Application.DTO.QueryDTO;
using Interview.Domain.Companies;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.CQRS.Queries;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Queries.Companies
{
    public class GetJobByCategoryQuery : IQuery<IEnumerable<JobDTO>>
    {
        public Guid CompanyId { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class GetJobByCategoryQueryHandler : IQueryHandler<GetJobByCategoryQuery, IEnumerable<JobDTO>>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public GetJobByCategoryQueryHandler(ICompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<JobDTO>> Handle(GetJobByCategoryQuery request, CancellationToken cancellationToken)
        {
            var company = _repository.FindOneById(_ => _.Id == request.CompanyId, cancellationToken);
            var jobCategory = company.FindJobCategory(request.CategoryId);
            if (company == null || jobCategory == null)
            {
                return Task.FromResult(new List<JobDTO>() as IEnumerable<JobDTO>);
            }
            var jobs = jobCategory.GetJobsFromCategory();
            var jobMapper = _mapper.Map<List<JobDTO>>(jobs) as IEnumerable<JobDTO>;
            return Task.FromResult(jobMapper);
        }
    }
}
