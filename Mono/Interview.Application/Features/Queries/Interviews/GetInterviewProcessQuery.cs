using Interview.Application.DTO.QueryDTO;
using Interview.Infrastructure.CQRS.Queries;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Queries.Interviews
{
    public class GetInterviewProcessQuery : IQuery<IEnumerable<InterviewProcessDTO>>
    {
        public Guid CollectionId { get; set; }
    }

    public class GetInterviewProcessQueryHandler : IQueryHandler<GetInterviewProcessQuery, IEnumerable<InterviewProcessDTO>>
    {
        private readonly IInterviewCollectionRepository _repository;

        public GetInterviewProcessQueryHandler(IInterviewCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InterviewProcessDTO>> Handle(GetInterviewProcessQuery request, CancellationToken cancellationToken)
        {
            var collection = _repository.FindOneById(_ => _.Id == request.CollectionId, cancellationToken);
            if (collection == null)
            {
                return null;
            }
            var processes = collection.Processes.Select(_ => new InterviewProcessDTO(_.Id, _.StepKey, _.Step));
            return processes;
        }
    }
}
