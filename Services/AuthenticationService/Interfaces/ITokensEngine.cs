using DTO;
using System;

namespace AuthenticationService.Interfaces
{
    /// <summary>
    /// Engine for working with the token system.
    /// </summary>
    public interface ITokensEngine
    {
        /// <summary>
        /// Generate token for user.
        /// </summary>
        UserToken GetToken(Guid userId);

        /// <summary>
        /// Check token is valid.
        /// </summary>
        OperationResult CheckToken(UserToken token);
    }
}
