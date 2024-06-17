using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Interviews
{
    public class UpdateInterviewCollectionNameCommand : ICommand<CommandResult<Guid>>
    {
        public Guid InterviewCollectionId { get; set; }
        public string CollectionName { get; set; }
    }

    public class UpdateInterviewCollectionNameCommandHandler : ICommandHandler<UpdateInterviewCollectionNameCommand, CommandResult<Guid>>
    {
        private readonly IInterviewCollectionRepository _repository;

        public UpdateInterviewCollectionNameCommandHandler(IInterviewCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult<Guid>> Handle(UpdateInterviewCollectionNameCommand request, CancellationToken cancellationToken)
        {
            var collection = _repository.FindOneById(_ => _.Id == request.InterviewCollectionId, cancellationToken);
            if (collection is null)
            {
                return CommandResult<Guid>.Error("Collection not found !");
            }
            collection.UpdateName(request.CollectionName);
            _repository.Update(collection);
            return CommandResult<Guid>.Success(collection.Id);
        }
    }
}
