using DTO;
using HttpWebRequestWrapperLib;
using System.Text.Json;

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
