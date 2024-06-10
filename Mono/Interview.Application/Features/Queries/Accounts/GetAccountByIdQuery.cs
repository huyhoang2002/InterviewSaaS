using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.CQRS.Queries;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Queries.Accounts
{
    public class GetAccountByIdQuery : IQuery<Account>
    {
        public string AccountId { get; set; }
    }

    public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, Account>
    {
        private readonly IAccountRepository _repository;

        public GetAccountByIdQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _repository.FindOneByIdAsync(_ => _.Id == request.AccountId, cancellationToken);
            return account;
        }
    }
}
