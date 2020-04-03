using GUI.ViewModels;
using DTO;
using HttpWebRequestWrapperLib;

namespace GUI.Scripts
{
    public class SignUpper
    {
        public static void SignUp(UserEmailPassword data)
        {
            new HttpWebRequestWrapper().Post("https://localhost:5011/user/create", null, data);
        }
    }
}
