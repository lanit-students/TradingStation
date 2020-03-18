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
        string GetToken(int userId);

        /// <summary>
        /// Delete token from storage.
        /// </summary>
        void DeleteToken(int userId);

        /// <summary>
        /// Check token is in storage.
        /// </summary>
        bool CheckToken(int userId, string token);
    }
}
