using Google.Apis.Auth;
using Interview.Application.DTO.AppSettingDTO;
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
        public AuthController(IConfiguration configuration, IOptions<UrlOption> options) 
        {
            _configuration = configuration;
            _options = options.Value;
        }

        [HttpGet("url")]
        public IActionResult GetGoogleSignInUrl()
        {
            return Ok(new
            {
                Url = $"{_options.DevelopmentUrl}/account/login"
            });
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
