using Interview.Infrastructure.Base.Interfaces;
using Interview.Infrastructure.Persistences.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected InterviewDbContext Context;
        protected DbSet<T> DbSet;
        public RepositoryBase(InterviewDbContext dbContext)
        {
            Context = dbContext;
            DbSet = Context.Set<T>();
        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> FindById(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual T FindOneById(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual async Task<T> FindOneByIdAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            var result = await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
            return result;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
