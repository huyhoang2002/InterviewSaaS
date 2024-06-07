using Interview.Infrastructure.Persistences.ApplicationDbContext;
using Interview.Infrastructure.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public InterviewDbContext Context;

        public UnitOfWork(InterviewDbContext context)
        {
            Context = context;
        }

        public async Task<T> SaveChangeAsync<T>(Func<Task<T>> action)
        {
            var result = await action();
            await Context.SaveChangesAsync();
            return result;
        }
    }
}
