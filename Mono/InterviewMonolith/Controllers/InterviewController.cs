﻿using Interview.Application.DTO.CommandDTO;
using Interview.Application.Features.Commands.Interviews;
using Interview.Application.Features.Queries.Interviews;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace InterviewMonolith.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public InterviewController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost]
        [Route("collection")]
        public async Task<IActionResult> CreateCollection([FromBody] CreateInterviewCollectionCommand command)
        {
            var result = (CommandResult)await _commandBus.SendAsync(command);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return CreatedAtAction(nameof(CreateCollection), result);
        }

        [HttpPost]
        [Route("collection/{collectionId}")]
        public async Task<IActionResult> AddStepToCollection(Guid collectionId, [FromBody] InterviewProcessDTO interviewProcessDTO)
        {
            var command = new AddStepToInterviewProcessCommand
            {
                InterviewCollectionId = collectionId,
                InterviewProcessDTO = interviewProcessDTO
            };
            var result = (CommandResult)await _commandBus.SendAsync(command);
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviewCollections()
        {
            var query = new GetInterviewCollectionQuery();
            var result = await _queryBus.SendAsync(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{collectionId}")]
        public async Task<IActionResult> GetInterviewProcess(Guid collectionId)
        {
            var query = new GetInterviewProcessQuery
            {
                CollectionId = collectionId
            };
            var result = await _queryBus.SendAsync(query);
            return Ok(result);
        }

        [HttpPut]
        [Route("{collectionId}")]
        public async Task<IActionResult> UpdateCollectionName(Guid collectionId, [FromBody] UpdateCollectionNameDTO updateCollectionNameDTO)
        {
            var command = new UpdateInterviewCollectionNameCommand
            {
                InterviewCollectionId = collectionId,
                CollectionName = updateCollectionNameDTO.CollectionName
            };
            var result = await _commandBus.SendAsync(command) as CommandResult<Guid>;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("{collectionId}/{stepId}")]
        public async Task<IActionResult> UpdateProcessStep(Guid collectionId, Guid stepId, [FromBody] UpdateStepDTO updateStepDTO)
        {
            var command = new UpdateInterviewProcessStepCommand()
            {
                CollectionId = collectionId,
                StepId = stepId,
                UpdateStepDTO = updateStepDTO
            };
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{collectionId}/{stepId}")]
        public async Task<IActionResult> DeleteProcessStep(Guid collectionId, Guid stepId)
        {
            var command = new DeleteStepFromProcessCommand
            {
                CollectionId = collectionId,
                StepId = stepId
            };
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{collectionId}")]
        public async Task<IActionResult> DeleteCollection(Guid collectionId)
        {
            var command = new DeleteCollectionCommand
            {
                CollectionId = collectionId
            };
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
 