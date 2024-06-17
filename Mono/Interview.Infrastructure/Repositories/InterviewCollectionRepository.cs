using Interview.Domain.Aggregates.Interviews;
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
    public class InterviewCollectionRepository : RepositoryBase<InterviewCollection>, IInterviewCollectionRepository
    {
        public InterviewCollectionRepository(InterviewDbContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<InterviewCollection> GetAll()
        {
            return DbSet
                .Include(_ => _.Processes)
                .Where(_ => _.IsDeleted == false);
        }

        public override InterviewCollection FindOneById(Expression<Func<InterviewCollection, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbSet
                .Include(_ => _.Processes)
                .Where(_ => _.IsDeleted == false)
                .FirstOrDefault(predicate);
        }
    }
}
