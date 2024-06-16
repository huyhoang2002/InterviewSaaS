using Interview.Domain.Aggregates.Interviews;
using Interview.Infrastructure.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories.Interfaces
{
    public interface IInterviewCollectionRepository : IRepositoryBase<InterviewCollection>
    {
    }
}
