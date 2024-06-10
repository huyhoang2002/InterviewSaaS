using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        void AddTokenToAccount(string accountId, Token token, CancellationToken cancellationToken);
    }
}
