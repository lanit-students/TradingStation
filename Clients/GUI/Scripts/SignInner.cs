using DTO;
using HttpWebRequestWrapperLib;

namespace GUI.Scripts
{
    public static class SignInner
    {
        public static void SignIn(UserEmailPassword userInput)
        {
            var requestWrapper = new HttpWebRequestWrapper();
            requestWrapper.Post("http://localhost:5001/authentication/login", null, userInput);
        }
    }
}
