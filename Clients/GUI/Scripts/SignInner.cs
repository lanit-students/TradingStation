using DTO;
using HttpWebRequestWrapperLib;
using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class SignInner
    {
        public static UserToken SignIn(UserEmailPassword userInput)
        {
            var wrapper = new HttpWebRequestWrapper();
            var response = wrapper.Post("https://localhost:5001/authentication/login", null, userInput);
            return JsonSerializer.Deserialize<UserToken>(response);
        }
    }
}
