using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class UserGetter
    {
        public static async Task<User> GetUserById(Guid id, string token)
        {
            var userToken = new UserToken() { UserId = id, Body = token };

            const string url = "https://localhost:5011/users/get";

            var query = new Dictionary<string, string>();

            query.Add("userId", id.ToString());

            var client = new RestClient<object, User>(url, RestRequestType.GET, userToken, queryParams: query);

            return await client.Execute();
        }
    }
}
