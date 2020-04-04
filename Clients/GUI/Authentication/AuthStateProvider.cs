using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace GUI.Authentication
{
    /// <summary>
    /// Custom implementation of AuthenticationStateProvider
    /// </summary>
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        public AuthStateProvider(ILocalStorageService localStorageService)
        {
            localStorage = localStorageService;
        }

        /// <summary>
        /// <see cref="AuthenticationStateProvider.GetAuthenticationStateAsync"/>
        /// </summary>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity(""));
            return await Task.FromResult(new AuthenticationState(anonymous));
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
            localStorage.ClearAsync();

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
