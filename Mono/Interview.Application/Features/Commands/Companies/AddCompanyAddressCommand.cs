using AutoMapper;
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
    public class AddCompanyAddressCommand : ICommand<CommandResult<Guid>>
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string? Province { get; set; }
        public Guid CompanyId { get; set; }
    }

    public class AddCompanyAddressCommandHandler : ICommandHandler<AddCompanyAddressCommand, CommandResult<Guid>>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public AddCompanyAddressCommandHandler(ICompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResult<Guid>> Handle(AddCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            var company = _repository.FindOneById(_ => _.Id == request.CompanyId, cancellationToken);
            if (company is not null)
            {
                var addressMapper = _mapper.Map<Address>(request);
                company.AddAddress(addressMapper);
                return CommandResult<Guid>.Success(addressMapper.Id);
            }
            else
            {
                return CommandResult<Guid>.Error("Failed to add address !");
            }
        }
    }
}
