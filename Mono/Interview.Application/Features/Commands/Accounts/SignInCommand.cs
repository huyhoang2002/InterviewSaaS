using Interview.Application.DTO.AppSettingDTO;
using Interview.Application.DTO.ResponseDTO;
using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.CQRS.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Features.Commands.Accounts
{
    public class SignInCommand : ICommand<CommandResult<TokenResponseDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInCommandHandler : ICommandHandler<SignInCommand, CommandResult<TokenResponseDTO>>
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly ILogger<SignInCommandHandler> _logger;
        private readonly UserManager<Account> _userManager;
        private readonly JwtOption _jwtOption;

        public SignInCommandHandler(SignInManager<Account> signInManager, ILogger<SignInCommandHandler> logger, UserManager<Account> userManager, IOptions<JwtOption> jwtOption)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _jwtOption = jwtOption.Value;
        }

        public async Task<CommandResult<TokenResponseDTO>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var account = await _userManager.FindByEmailAsync(request.Email);
            if (account == null)
            {
                return CommandResult<TokenResponseDTO>.Error("Account is not found");
            }
            var roles = await _userManager.GetRolesAsync(account);
            var signIn = await _signInManager.PasswordSignInAsync(account, request.Password, false, false);
            if (signIn.Succeeded == false)
            {
                return CommandResult<TokenResponseDTO>.Error("Email or password are invalid !");
            }
            var accessToken = generateAccessToken(request, roles);
            var refreshToken = generateRefreshToken();
            account.StoreToken(accessToken, refreshToken, false, account.Id);
            return CommandResult<TokenResponseDTO>.Success(new TokenResponseDTO(accessToken, refreshToken, "Bearer"));
        }

        private string generateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string generateAccessToken(SignInCommand request, IList<string> roles)
        {
            var issuer = _jwtOption.Issuer;
            var audience = _jwtOption.Audience;
            var key = Encoding.ASCII.GetBytes(_jwtOption.Key);
            var claims = new List<Claim>
                {

                    new Claim(JwtRegisteredClaimNames.Email, request.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_jwtOption.ExpireIn),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return accessToken;
        }
    }
}
