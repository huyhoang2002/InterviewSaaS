using Interview.Domain.Companies;
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
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(InterviewDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Company> GetCompanies()
        {
            return DbSet
                .Where(_ => _.IsDeleted == false)
                .Include(_ => _.CompanyAddresses)
                .Include(_ => _.JobCategories);
        }

        public override async Task<Company> FindOneByIdAsync(Expression<Func<Company, bool>> predicate, CancellationToken cancellationToken)
        {
            return await DbSet
                .Include(_ => _.CompanyAddresses)
                .Include(_ => _.JobCategories)
                .FirstOrDefaultAsync(_ => _.IsDeleted == false);
        }
    }
}
