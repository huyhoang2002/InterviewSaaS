using Interview.Application.Features.Commands.User;
using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.CQRS.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USER")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(new List<string>());
        }

        [HttpGet("verify")]
        public async Task<IActionResult> GetUsersVerify()
        {
            return Ok();
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
                _logger.LogError("Update user Failed !");
                return BadRequest(result);
            }
            else
            {
                _logger.LogInformation("Update user success !");
                return Ok(result);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            _logger.LogInformation($"Deleting user {id}");
            var deleteUserCommand = new DeleteUserCommand
            {
                Id = id
            };
            var result = await _commandBus.SendAsync(deleteUserCommand) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
