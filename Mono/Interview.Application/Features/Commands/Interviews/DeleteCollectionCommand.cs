using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Interviews
{
    public class DeleteCollectionCommand : ICommand<CommandResult<Guid>>
    {
        public Guid CollectionId { get; set; }
    }

    public class DeleteCollectionCommandHandler : ICommandHandler<DeleteCollectionCommand, CommandResult<Guid>>
    {
        private readonly IInterviewCollectionRepository _repository;

        public DeleteCollectionCommandHandler(IInterviewCollectionRepository repository)
        {
            _repository = repository;
        }
        public async Task<CommandResult<Guid>> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = _repository.FindOneById(_ => _.Id == request.CollectionId, cancellationToken);
            if (collection is null)
            {
                return CommandResult<Guid>.Error("Collection is not found");
            }
            collection.RemoveCollection();
            return CommandResult<Guid>.Success(collection.Id);
        }
    }
}
