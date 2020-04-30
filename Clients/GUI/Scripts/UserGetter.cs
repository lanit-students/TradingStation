using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class UserGetter
    {
        public static async Task<UserInfoRequest> GetUserById(UserToken userToken)
        {
            const string url = "https://localhost:5011/users/get";

            var client = new RestClient<object, UserInfoRequest>(url, RestRequestType.GET, userToken);

            return await client.ExecuteAsync();
        }
    }
}
