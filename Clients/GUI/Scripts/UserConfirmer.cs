using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class UserConfirmer
    {
        public static async Task<bool> UserConfirm(string secretToken)
        {
            string url = $"http://localhost:5010/users/confirm?secretToken={secretToken}";

            var client = new RestClient<string, bool>(url, RestRequestType.GET);

            return await client.ExecuteAsync();
        }
    }
}
