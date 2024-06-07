using Interview.Domain.Aggregates.User;
using Interview.Infrastructure.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}
