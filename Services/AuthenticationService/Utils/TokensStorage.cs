using System;
using System.Collections.Generic;

namespace AuthenticationService
{
    public static class TokensStorage
    {
        /// <summary>
        /// Stores valid tokens
        /// </summary>
        private static Dictionary<int, string> tokens = new Dictionary<int, string>();

     
        /// <summary>
        /// Generates a valid token
        /// </summary>
        public static string GetToken(int id)
        {
                var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                tokens[id] = token;
                return tokens[id];    
        }

        /// <summary>
        /// Checks if token is valid
        /// </summary>
        public static bool CheckToken(int id,string token)
            => (tokens.ContainsKey(id) && tokens[id] == token);


        /// <summary>
        /// Delete token from storage, it becomes invalid
        /// </summary>
        public static void DeleteToken(int id, string token)
        {
            if (tokens.ContainsKey(id) && tokens[id] == token)
            {
                tokens.Remove(id);
            }
        }
    }
}
