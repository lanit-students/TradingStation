using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GUI.Authentication
{
    /// <summary>
    /// Custom implementation of AuthenticationStateProvider
    /// </summary>
    public class AuthStateProvider : AuthenticationStateProvider
    {
        /// <summary>
        /// <see cref="AuthenticationStateProvider.GetAuthenticationStateAsync"/>
        /// </summary>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var notAnonymous = new ClaimsPrincipal(new ClaimsIdentity("test"));

            return await Task.FromResult(new AuthenticationState(notAnonymous));
        }

        /// <summary>
        /// Signs current user out
        /// </summary>
        public void MarkSignedIn()
        {
            // TODO: implement real logic using api

            var identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "0"),
                    new Claim(ClaimTypes.GivenName, "Admin's first name"),
                    new Claim(ClaimTypes.Surname, "Admin's last name"),
                    new Claim(ClaimTypes.DateOfBirth, "xxxxx"),
                    new Claim(ClaimTypes.Email, "root@gmail.com")
                }, "Fake authentication type");

            var claims = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }

        /// <summary>
        /// Signs current user in
        /// </summary>
        public void MarkSignedOut()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
