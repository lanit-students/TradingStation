using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AuthenticationService.Interfaces;
using DTO;
using Kernel.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService
{
    /// <inheritdoc />
    public class TokensEngine : ITokensEngine
    {
        private SecurityKey key;
        private ILogger<TokensEngine> logger;

        public TokensEngine([FromServices] ILogger<TokensEngine> logger)
        {
            this.logger = logger;
            var hmac = new HMACSHA256();
            key = new SymmetricSecurityKey(hmac.Key);
        }

        /// <inheritdoc />
        public UserToken GetToken(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                logger.LogWarning(new BadRequestException(), "Attempt to get token by empty id.");
                throw new BadRequestException();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            logger.LogInformation($"User {userId} got a token.");

            return new UserToken
            {
                UserId = userId,
                Body = token
            };
        }

        /// <inheritdoc />
        public OperationResult CheckToken(UserToken token)
        {
            try
            {
                logger.LogInformation($"Checking {token.Body} token for user {token.UserId}.");

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token.Body, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key
                }, out var validatedToken);
            }
            catch
            {
                logger.LogWarning($"{token.Body} is not a valid token for user {token.UserId}.");
                return new OperationResult() { IsSuccess = false };
            }

            logger.LogInformation($"User {token.UserId} with token {token.Body} approved.");
            return new OperationResult() { IsSuccess = true };
        }
    }
}
