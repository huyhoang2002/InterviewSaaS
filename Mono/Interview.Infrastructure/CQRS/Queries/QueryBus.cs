using Interview.Infrastructure.UnitOfWork;
using Interview.Infrastructure.UnitOfWork.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.CQRS.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public QueryBus(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<object> SendAsync(object request)
        {
            return await _unitOfWork.SaveChangeAsync(() => _mediator.Send(request));
        }
    }
}
