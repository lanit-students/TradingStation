using System.Threading.Tasks;
using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;

namespace GUI.Scripts
{
    public static class UserEditor
    {
        /// <summary>
        /// Edits information about the user
        /// </summary>
        /// <param name="id">Identificator no know who to change</param>
        /// <param name="userInfo">Updated information</param>
        /// <param name="oldPassword">If not null, there is an attempt to change password</param>
        /// <param name="newPassword">Same as with oldPassword</param>
        public static async Task EditUser(UserToken userToken, EditUserRequest request)
        {
            const string url = "https://localhost:5011/users/edit";

            var client = new RestClient<EditUserRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }
    }
}
