using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.Base;
using Interview.Infrastructure.Persistences.ApplicationDbContext;
using Interview.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(InterviewDbContext dbContext) : base(dbContext)
        {
        }

        public void AddTokenToAccount(string accountId, Token token, CancellationToken cancellationToken)
        {
            var account = DbSet.Include(_ => _.Tokens).FirstOrDefault(_ => _.Id == accountId);
            if (account is not null)
            {
                account.StoreToken(token.AccessToken, token.RefreshToken, token.BlagFlag, token.AccountId);
            }
        }

        public override async Task<Account> FindOneByIdAsync(Expression<Func<Account, bool>> predicate, CancellationToken cancellationToken)
        {
            return await DbSet.Include(_ => _.Tokens).FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
