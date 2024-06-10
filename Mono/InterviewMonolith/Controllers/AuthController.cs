using Google.Apis.Auth;
using Interview.Application.DTO.AppSettingDTO;
using Interview.Application.Features.Commands.Accounts;
using Interview.Infrastructure.CQRS.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Management;

namespace InterviewMonolith.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UrlOption _options;
        private readonly ICommandBus _commandBus;

        public AuthController(IConfiguration configuration, IOptions<UrlOption> options, ICommandBus commandBus) 
        {
            _configuration = configuration;
            _options = options.Value;
            _commandBus = commandBus;
        }

        [HttpGet("external-url")]
        public IActionResult GetGoogleSignInUrl()
        {
            return Ok(new
            {
                Url = $"{_options.DevelopmentUrl}/account/login"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("/account/login")]
        public IActionResult SignInWithGoogle()
        {            
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "https://localhost:3000"
            }, GoogleDefaults.AuthenticationScheme); 
        }

        [HttpGet("google-access-token")]
        public async Task<IActionResult> GetGoogleAccessToken()
        {

            var authenticateResult = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            var accessToken = authenticateResult.Properties.GetTokenValue("access_token");  
            if (accessToken is not null)
            {
                return Ok(accessToken);
            }
            return BadRequest();
        }
    }
}
