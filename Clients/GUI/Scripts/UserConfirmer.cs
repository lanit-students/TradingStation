using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class UserConfirmer
    {
        public static async Task<bool> UserConfirm(string secretToken)
        {
            string url = $"https://localhost:5011/users/confirm?secretToken={secretToken}";

            var client = new RestClient<string, bool>(url, RestRequestType.GET);

            return await client.ExecuteAsync();
        }
    }
}
