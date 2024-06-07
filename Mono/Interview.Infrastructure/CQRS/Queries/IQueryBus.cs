using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.CQRS.Queries
{
    public interface IQueryBus<T>
    {
        public T Run(Func<T> query);
    }
}
