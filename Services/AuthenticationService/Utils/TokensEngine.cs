using System;
using System.Collections.Generic;

using AuthenticationService.Interfaces;
using DTO;

namespace AuthenticationService
{
    /// <summary>
    /// Engine for working with the token system.
    /// </summary>
    public class TokensEngine : ITokensEngine
    {
        /// <summary>
        /// Tokens storage.
        /// </summary>
        private Dictionary<Guid, UserToken> tokens = new Dictionary<Guid, UserToken>();

        /// <summary>
        /// Generate token for user and put it in storage.
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <returns>New token for user</returns>
        public UserToken GetToken(Guid userId)
        {
            UserToken token = new UserToken() { Body = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) };

            tokens[userId] = token;

            return tokens[userId];    
        }

        /// <summary>
        /// Check token is in storage.
        /// </summary>
        public bool CheckToken(UserToken token)
        {
            return tokens.TryGetValue(token.UserId, out UserToken tokenFromStorage) && tokenFromStorage.Body == token.Body;
        }

        /// <summary>
        /// Delete token from storage.
        /// </summary>
        public void DeleteToken(Guid userId)
        {
            if (tokens.ContainsKey(userId))
            {
                tokens.Remove(userId);
            }
        }
    }
}
