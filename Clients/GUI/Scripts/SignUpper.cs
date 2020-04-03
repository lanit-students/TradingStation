using GUI.ViewModels;
using DTO;
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
