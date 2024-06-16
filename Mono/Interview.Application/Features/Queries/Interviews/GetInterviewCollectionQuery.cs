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
    public class GetInterviewCollectionQuery : IQuery<IEnumerable<InterviewCollectionDTO>>
    {
    }

    public class GetInterviewCollectionQueryHandler : IQueryHandler<GetInterviewCollectionQuery, IEnumerable<InterviewCollectionDTO>>
    {
        private readonly IInterviewCollectionRepository _repository;

        public GetInterviewCollectionQueryHandler(IInterviewCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InterviewCollectionDTO>> Handle(GetInterviewCollectionQuery request, CancellationToken cancellationToken)
        {
            var collections = _repository.GetAll();
            return collections.Select(_ => new InterviewCollectionDTO(_.Id, _.CollectionName, _.IsApplied));
        }
    }
}
