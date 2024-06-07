﻿using Interview.Application.Features.Commands.User;
using Interview.Infrastructure.CQRS.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewMonolith.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ILogger<UserController> _logger;
        public UserController(ICommandBus commandBus, ILogger<UserController> logger)
        {
            _commandBus = commandBus;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddUser([FromBody] UserCommand command)
        {
            _logger.LogInformation("Adding a User !");
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                _logger.LogError("Add user failed !");
                return BadRequest(result);
            }
                
            else
            {
                _logger.LogInformation("Add user success !");
                return Ok(result);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            _logger.LogInformation($"Updating user {command.UserId}");
            command.UserId = id;
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
                _logger.LogError("Update user Failed !");
            }
            else
            {
                _logger.LogInformation("Update user success !");
                return Ok(result);
            }
        }
    }
}
