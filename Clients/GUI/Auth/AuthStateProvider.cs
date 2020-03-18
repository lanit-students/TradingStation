using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GUI.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        // We will pass user object here from sign in i guess
        public void MarkSignedIn()
        {
            var identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "admin@gmail.com"),
                    new Claim(ClaimTypes.Name, "Admin's first name"),
                    new Claim(ClaimTypes.Surname, "Admin's second name"),
                    new Claim(ClaimTypes.DateOfBirth, "Birthday"),
                    new Claim(ClaimTypes.CookiePath, "")
                }, "Fake authentication type");

            var claims = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(claims);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        public void MarkSignedOut()
            =>NotifyAuthenticationStateChanged(Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }
}
