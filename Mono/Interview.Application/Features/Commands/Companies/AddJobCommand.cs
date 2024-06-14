using AutoMapper;
using Interview.Application.DTO.CommandDTO;
using Interview.Domain.Companies;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Companies
{
    public class AddJobCommand : ICommand<CommandResult<Guid>>
    {
        public Guid CompanyId { get; set; }
        public Guid JobCategoryId { get; set; }
        public JobDTO JobDTO { get; set; }
    }

    public class AddJobCommandHandler : ICommandHandler<AddJobCommand, CommandResult<Guid>>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public AddJobCommandHandler(ICompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResult<Guid>> Handle(AddJobCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.FindOneByIdAsync(_ => _.Id == request.CompanyId, cancellationToken);
            if (company is null)
            {
                return CommandResult<Guid>.Error("Company is not found");
            }
            var jobCategory = company.FindJobCategory(request.JobCategoryId);
            if (jobCategory is null)
            {
                return CommandResult<Guid>.Error("Job category is not found");
            }
            var jobMapper = _mapper.Map<Job>(request.JobDTO);
            jobMapper.CompanyId = request.CompanyId;
            jobMapper.JobCategoryId = request.JobCategoryId;
            company.AddJob(jobMapper);
            return CommandResult<Guid>.Success(company.Id);
        }
    }
}

