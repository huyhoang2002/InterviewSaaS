using Interview.Application.DTO.AppSettingDTO;
using Interview.Application.DTO.ResponseDTO;
using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
    public class RefreshTokenCommand : ICommand<CommandResult<TokenResponseDTO>>
    {
        public string AccountId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, CommandResult<TokenResponseDTO>>
    {
        private readonly IAccountRepository _repository;
        private readonly JwtOption _jwtOption;

        public RefreshTokenCommandHandler(IAccountRepository repository, IOptions<JwtOption> jwtOption)
        {
            _repository = repository;
            _jwtOption = jwtOption.Value;
        }

        public async Task<CommandResult<TokenResponseDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.FindOneByIdAsync(_ => _.Id == request.AccountId, cancellationToken);
            var token = account.GetActiveToken(account.Id);
            var verifyRefreshToken = account.CompareRefreshToken(token, request.RefreshToken);
            if (account is null || token is null || verifyRefreshToken is false)
            {
                return CommandResult<TokenResponseDTO>.Error("Failed to refresh token !");
            }
            var pricipal = getPrincipalFromExpiredToken(request.AccessToken);
            if (pricipal is null)
            {
                return CommandResult<TokenResponseDTO>.Error("Failed to refresh token !");
            }
            var newAccessToken = generateAccessToken(pricipal.Claims.ToList());
            var newRefreshToken = generateRefreshToken();
            account.RefreshToken(account.Id, newAccessToken, newRefreshToken);
            _repository.Update(account);
            return CommandResult<TokenResponseDTO>.Success(new TokenResponseDTO(newAccessToken, newRefreshToken, "Bearer"));
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

        private string generateAccessToken(List<Claim> authClaims)
        {
            var key = Encoding.ASCII.GetBytes(_jwtOption.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddDays(_jwtOption.ExpireIn),
                Issuer = _jwtOption.Issuer,
                Audience = _jwtOption.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return accessToken;
        }

        private ClaimsPrincipal? getPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
