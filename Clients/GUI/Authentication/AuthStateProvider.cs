using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GUI.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            return await Task.FromResult(new AuthenticationState(anonymous));
        }

        public void MarkSignedIn()
        {
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

        public void MarkSignedOut()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
