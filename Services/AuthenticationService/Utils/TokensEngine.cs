using System;
using System.Collections.Generic;

using AuthenticationService.Interfaces;
using DTO;
using Kernel.CustomExceptions;

namespace AuthenticationService
{
    /// <inheritdoc />
    public class TokensEngine : ITokensEngine
    {
        /// <summary>
        /// Tokens storage.
        /// </summary>
        private Dictionary<Guid, string> tokens = new Dictionary<Guid, string>();

        /// <inheritdoc />
        public UserToken GetToken(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new BadRequestException();

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            tokens[userId] = token;

            return new UserToken
            {
                UserId = userId,
                Body = token
            };
        }

        /// <inheritdoc />
        public OperationResult CheckToken(UserToken token)
        {
            var result = new OperationResult
            {
                IsSuccess = tokens.TryGetValue(token.UserId, out string tokenFromStorage) && tokenFromStorage == token.Body
            };

            return result;
        }

        /// <inheritdoc />
        public OperationResult DeleteToken(Guid userId)
        {
            var result = new OperationResult();

            if (tokens.ContainsKey(userId))
            {
                tokens.Remove(userId);

                result.IsSuccess = true;
            }

            return result;
        }
    }
}
