using Interview.Application.DTO.CommandDTO;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Interviews
{
    public class UpdateInterviewProcessStepCommand : ICommand<CommandResult<Guid>>
    {
        public Guid CollectionId { get; set; }
        public Guid StepId { get; set; }
        public UpdateStepDTO UpdateStepDTO { get; set; }
    }

    public class UpdateInterviewProcessStepCommandHandler : ICommandHandler<UpdateInterviewProcessStepCommand, CommandResult<Guid>>
    {
        private readonly IInterviewCollectionRepository _repository;
        public UpdateInterviewProcessStepCommandHandler(IInterviewCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult<Guid>> Handle(UpdateInterviewProcessStepCommand request, CancellationToken cancellationToken)
        {
            var collection = _repository.FindOneById(_ => _.Id == request.CollectionId, cancellationToken);
            var step = collection.GetProcess(request.StepId);
            if (collection is null || step is null)
            {
                return CommandResult<Guid>.Error("Collection or step is not found");
            }
            step.UpdateStep(new Domain.Aggregates.Interviews.InterviewProcess(
                    request.UpdateStepDTO.StepKey,
                    request.UpdateStepDTO.StepName
                ));
            _repository.Update(collection);
            return CommandResult<Guid>.Success(collection.Id);
        }
    }
}
