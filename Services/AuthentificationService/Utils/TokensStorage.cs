using System;
using System.Collections.Generic;

namespace AuthenticationService
{
    public static class TokensStorage
    {
        /// <summary>
        /// Stores valid tokens
        /// </summary>
        private static List<string> tokens = new List<string>() { null };

        /// <summary>
        /// Gets index by token value
        /// </summary>
        public static int BinarySearch(string token)
        {
            int left = 0;
            int right = tokens.Count - 1;
            int middle;
            int compare;
            while (left < right)
            {
                middle = (left + right) / 2;

                compare = string.Compare(tokens[middle], token);
                if (compare < 0)
                {
                    left = middle + 1;
                }
                if (compare > 0)
                {
                    right = middle - 1;
                }
                if (compare == 0)
                {
                    return middle;
                }
            }
            return left;
        }

        /// <summary>
        /// Generates a valid token
        /// </summary>
        public static string Get()
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            int index = BinarySearch(token);
            var compare = string.Compare(tokens[index], token);
            if (compare < 0) { index++; }
            tokens.Insert(index, token);
            return token;
        }

        /// <summary>
        /// Checks if token is valid
        /// </summary>
        public static bool Check(string token)
        {
            int index = BinarySearch(token);
            return string.Compare(tokens[index], token) == 0;
        }

        /// <summary>
        /// Delete token from storage, it becomes invalid
        /// </summary>
        public static void Delete(string token)
        {
            int index = BinarySearch(token);
            tokens.RemoveAt(index);
        }
    }
}
