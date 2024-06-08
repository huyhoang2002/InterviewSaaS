using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewMonolith.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController() { }

        [HttpGet("url")]
        //[Authorize]
        public IActionResult GetGoogleSignInUrl()
        {
            return Ok(new
            {
                Url = "https://localhost:7212/account/login"
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
    }
}
