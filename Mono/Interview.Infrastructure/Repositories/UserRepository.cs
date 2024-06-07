using Interview.Domain.Aggregates.User;
using Interview.Infrastructure.Base;
using Interview.Infrastructure.Persistences.ApplicationDbContext;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(InterviewDbContext dbContext) : base(dbContext)
        {
        }
    }
}
