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
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public IEnumerable<T> GetAll();
        public IQueryable<T> FindById(Expression<Func<T, bool>> predicate);
        public T FindOneById(Expression<Func<T, bool>> predicate);
    }
}
