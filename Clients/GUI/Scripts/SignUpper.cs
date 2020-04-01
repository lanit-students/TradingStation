using GUI.GUIModels;
using System.Net;
using DTO;
using System.IO;
using System.Text.Json;
using HttpWebRequestWrapperLib;

namespace GUI.Scripts
{
    public class SignUpper
    {
        public static void SignUp(SignUpViewModel input)
        {
            var output = new UserEmailPassword(input.Email, input.Password);
            new HttpWebRequestWrapper().Post("https://localhost:5011/user/create", null, output);
        }
    }
}
