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
using System.Windows.Input;

namespace Interview.Application.Features.Commands.Companies
{
    public class UpdateJobCommand : ICommand<CommandResult<Guid>>
    {
        public Guid CompanyId { get; set; }
        public Guid JobId { get; set; }
        public JobDTO JobDTO { get; set; }
    }

    public class UpdateJobCompanyHandler : ICommandHandler<UpdateJobCommand, CommandResult<Guid>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public UpdateJobCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CommandResult<Guid>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var company = _companyRepository.FindOneById(_ => _.Id == request.CompanyId, cancellationToken);
            if (company == null)
            {
                return CommandResult<Guid>.Error("Company is not found !");
            }
            var job = company.GetJob(request.JobId);
            if (job is null)
            {
                return CommandResult<Guid>.Error("Job is not found");
            }
            var jobUpdate = new Job(
                    request.JobDTO.JobName,
                    request.JobDTO.JobDescription,
                    request.JobDTO.Level,
                    request.JobDTO.Position,
                    request.JobDTO.MinExp,
                    request.JobDTO.MaxExp
                );
            job.Update(jobUpdate);
            _companyRepository.Update(company);
            return CommandResult<Guid>.Success(company.Id);
        }
    }
}
