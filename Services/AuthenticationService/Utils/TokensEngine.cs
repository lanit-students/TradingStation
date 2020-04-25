using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AuthenticationService.Interfaces;
using DTO;
using Kernel.CustomExceptions;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService
{
    /// <inheritdoc />
    public class TokensEngine : ITokensEngine
    {
        private SecurityKey key;

        public TokensEngine()
        {
            var hmac = new HMACSHA256();
            key = new SymmetricSecurityKey(hmac.Key);
        }

        /// <inheritdoc />
        public UserToken GetToken(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new BadRequestException();

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
                return new OperationResult() { IsSuccess = false };
            }

            return new OperationResult() { IsSuccess = true };
        }
    }
}
