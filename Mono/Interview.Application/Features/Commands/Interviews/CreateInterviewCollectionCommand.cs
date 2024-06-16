using Interview.Domain.Aggregates.Interviews;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Interviews
{
    public class CreateInterviewCollectionCommand : ICommand<CommandResult<Guid>>
    {
        public string CollectionName { get; set; }
    }

    public class CreateInterviewCollectionCommandHandler : ICommandHandler<CreateInterviewCollectionCommand, CommandResult<Guid>>
    {
        private readonly IInterviewCollectionRepository _repository;

        public CreateInterviewCollectionCommandHandler(IInterviewCollectionRepository repository)
        {
            _repository = repository;
        }

        public Task<CommandResult<Guid>> Handle(CreateInterviewCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = _repository.FindOneById(_ => _.IsApplied == true, cancellationToken);
            collection?.ResetApplyStatus();
            var newInterviewCollection = new InterviewCollection(request.CollectionName, true);
            _repository.Add(newInterviewCollection);
            return Task.FromResult(CommandResult<Guid>.Success(newInterviewCollection.Id));
        }
    }
}
