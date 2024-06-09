using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Base.Interfaces
{
    public interface IRepositoryBase<T>
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        IQueryable<T> FindById(Expression<Func<T, bool>> predicate);
        T FindOneById(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T> FindOneByIdAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
