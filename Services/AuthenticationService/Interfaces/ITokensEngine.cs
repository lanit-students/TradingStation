﻿using DTO;
using System;

namespace AuthenticationService.Interfaces
{
    /// <summary>
    /// Engine for working with the token system.
    /// </summary>
    public interface ITokensEngine
    {
        /// <summary>
        /// Generate token for user and put it in storage.
        /// </summary>
        UserToken GetToken(Guid userId);

        /// <summary>
        /// Delete token from storage.
        /// </summary>
        void DeleteToken(Guid userId);

        /// <summary>
        /// Check token is in storage.
        /// </summary>
        bool CheckToken(UserToken token);
    }
}
