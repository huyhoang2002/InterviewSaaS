using AutoMapper;
using Interview.Application.DTO.CommandDTO;
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
    public class AddStepToInterviewProcessCommand : ICommand<CommandResult<Guid>>
    {
        public Guid InterviewCollectionId { get; set; }
        public InterviewProcessDTO InterviewProcessDTO { get; set; }
    }

    public class AddStepToInterviewProcessCommandHandler : ICommandHandler<AddStepToInterviewProcessCommand, CommandResult<Guid>>
    {
        private readonly IInterviewCollectionRepository _repository;
        private readonly IMapper _mapper;

        public AddStepToInterviewProcessCommandHandler(IInterviewCollectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResult<Guid>> Handle(AddStepToInterviewProcessCommand request, CancellationToken cancellationToken)
        {
            var collection = _repository.FindOneById(_ => _.Id == request.InterviewCollectionId, cancellationToken);
            if (collection == null)
            {
                return CommandResult<Guid>.Error("Collection not found");
            }
            var step = new InterviewProcess(
                    request.InterviewProcessDTO.StepKey,
                    request.InterviewProcessDTO.Step,
                    collection.Id
                );
            collection.AddStep(step);
            return CommandResult<Guid>.Success(collection.Id);
        }
    }
}
