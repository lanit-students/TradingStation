using Kernel.CustomExceptions;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using UserService.Interfaces;

namespace UserService.Utils
{
    public class SecretTokenEngine : ISecretTokenEngine
    {
        private readonly ILogger<SecretTokenEngine> logger;
        public SecretTokenEngine([FromServices] ILogger<SecretTokenEngine> logger)
        {
            this.logger = logger;
        }
        private Dictionary<Guid, String> secretTokens = new Dictionary<Guid, String>();

        public Guid GetToken(string email)
        {
            Guid token = Guid.NewGuid();
            secretTokens[token] = email;
            return token;
        }

        public string GetEmail(Guid token)
        {
            if (secretTokens.TryGetValue(token, out string value))
            {
                string email;
                email= secretTokens[token];
                secretTokens.Remove(token);
                return email;
            }
            var e = new NotFoundException("Not Found user to confirm.");
            logger.LogWarning(e, "NotFound thrown while trying to confirm User.");
            throw e;
        }

    }
}
