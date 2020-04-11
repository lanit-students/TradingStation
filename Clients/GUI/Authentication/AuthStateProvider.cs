using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Blazored.SessionStorage;

namespace GUI.Authentication
{
    /// <summary>
    /// Custom implementation of AuthenticationStateProvider
    /// </summary>
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService storage;

        public AuthStateProvider(ISessionStorageService sessionStorageService)
        {
            storage = sessionStorageService;
        }

        /// <summary>
        /// <see cref="AuthenticationStateProvider.GetAuthenticationStateAsync"/>
        /// </summary>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var id = await storage.GetItemAsync<Guid>("id");
            var user = new ClaimsPrincipal(new ClaimsIdentity(id == Guid.Empty ? "" : "auth"));
            return await Task.FromResult(new AuthenticationState(user));
        }

        /// <summary>
        /// Signs current user out
        /// </summary>
        public void MarkSignedIn()
        {
            var identity = new ClaimsIdentity("auth");

            var claims = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }

        /// <summary>
        /// Signs current user in
        /// </summary>
        public void MarkSignedOut()
        {
            storage.ClearAsync();

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
