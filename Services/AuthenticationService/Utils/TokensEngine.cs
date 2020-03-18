using System;
using System.Collections.Generic;

using AuthenticationService.Interfaces;

namespace AuthenticationService
{
    /// <inheritdoc />
    public class TokensEngine : ITokensEngine
    {
        /// <summary>
        /// Tokens storage.
        /// </summary>
        private Dictionary<int, string> tokens = new Dictionary<int, string>();

        /// <inheritdoc />
        public string GetToken(int userId)
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            tokens[userId] = token;

            return tokens[userId];    
        }

        /// <inheritdoc />
        public bool CheckToken(int userId, string token)
        {
            return tokens.TryGetValue(userId, out string tokenFromStorage) && tokenFromStorage == token;
        }

        /// <inheritdoc />
        public void DeleteToken(int userId)
        {
            if (tokens.ContainsKey(userId))
            {
                tokens.Remove(userId);
            }
        }
    }
}
