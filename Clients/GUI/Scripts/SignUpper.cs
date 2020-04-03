using DTO;
using Kernel;
using Kernel.Enums;

namespace GUI.Scripts
{
    public static class SignUpper
    {
        public static void SignUp(UserEmailPassword data)
        {
            const string url = "https://localhost:5011/user/create";

            var result = new RestClient<UserEmailPassword, bool>(url, RestRequestType.POST).Execute(data);
        }
    }
}
