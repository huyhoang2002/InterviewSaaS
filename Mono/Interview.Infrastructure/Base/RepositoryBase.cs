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
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
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

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> FindById(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public T FindOneById(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
