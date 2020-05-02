using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class SignUpper
    {
        public static async Task SignUp(CreateUserRequest user)
        {
            const string url = "https://localhost:5011/users/create";

            var client = new RestClient<CreateUserRequest, bool>(url, RestRequestType.POST);

            await client.ExecuteAsync(user);
        }
    }
}
