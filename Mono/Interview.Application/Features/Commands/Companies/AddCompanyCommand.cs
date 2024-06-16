using AutoMapper;
using Interview.Application.Behaviors.Validators;
using Interview.Application.DTO.CommandDTO;
using Interview.Domain.Aggregates.Companies;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interview.Application.Features.Commands.Companies
{
    public class AddCompanyCommand : ICommand<CommandResult<Guid>>
    {
        public string CompanyName { get; set; }
        public string CompanyLogoUrl { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyDomain { get; set; }
        public string Email { get; set; }
        public string CompanyPhoneNumber { get; set; }
    }

    public class AddCompanyCommandHandler : ICommandHandler<AddCompanyCommand, CommandResult<Guid>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddCompanyCommandHandler> _logger;

        public AddCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<AddCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<CommandResult<Guid>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var companyMapper = _mapper.Map<Company>(request);
                var validator = new AddCompanyValidator();
                var result = validator.Validate(companyMapper);
                if (result.IsValid)
                {
                    await _companyRepository.AddAsync(companyMapper);
                    return CommandResult<Guid>.Success(companyMapper.Id);
                }
                else
                {
                    return CommandResult<Guid>.Error("Validation failed !");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResult<Guid>.Error("Bad request");
            }
        }
    }
}
