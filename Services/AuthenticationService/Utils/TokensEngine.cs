using System;
using System.Collections.Generic;

using AuthenticationService.Interfaces;
using DTO;

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
        public string GetToken(Guid userId)
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            tokens[userId] = token;

            return tokens[userId];    
        }

        /// <inheritdoc />
        public bool CheckToken(UserToken token)
        {
            return tokens.TryGetValue(token.UserId, out string tokenFromStorage) && tokenFromStorage == token.Body;
        }

        /// <inheritdoc />
        public void DeleteToken(Guid userId)
        {
            if (tokens.ContainsKey(userId))
            {
                tokens.Remove(userId);
            }
        }
    }
}
