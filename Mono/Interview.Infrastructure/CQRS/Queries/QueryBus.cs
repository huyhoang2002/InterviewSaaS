using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.CQRS.Queries
{
    public class QueryBus<T> : IQueryBus<T>
    {
        public T Run(Func<T> query)
        {
            return query();
        }
    }
}
