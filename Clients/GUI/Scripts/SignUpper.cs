using DTO;
using HttpWebRequestWrapperLib;
using System;
using System.Text.Json;

namespace GUI.Scripts
{
    public static class SignUpper
    {
        public static Guid SignUp(UserEmailPassword data)
        {
            var wrapper = new HttpWebRequestWrapper();
            var response = wrapper.Post("https://localhost:5011/user/create", null, data);
            return JsonSerializer.Deserialize<Guid>(response);
        }
    }
}
