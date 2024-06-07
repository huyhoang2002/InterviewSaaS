using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.CQRS.Commands
{
    public interface ICommandBus
    {
        Task<object> SendAsync(object request);
    }
}
