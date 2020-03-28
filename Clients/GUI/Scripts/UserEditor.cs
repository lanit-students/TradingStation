using System;
using System.Text.Json;
using System.Net;
using System.IO;
using DTO;

namespace GUI.Scripts
{
    public class UserEditor
    {
        /// <summary>
        /// Edits information about the user
        /// </summary>
        /// <param name="id">Identificator no know who to change</param>
        /// <param name="userInfo">Updated information</param>
        /// <param name="oldPassword">If not null, there is an attempt to change password</param>
        /// <param name="newPassword">Same as with oldPassword</param>
        public static void EditUser(Guid id, UserInformation userInfo, string oldPassword = null, string newPassword = null)
        {
            // The edit user stuff is in process in another team
            var request = (HttpWebRequest)WebRequest.Create("https://localhost:5011/user/edit");
            request.Method = "POST";
            var jsonOutput = JsonSerializer.Serialize(userInfo);
            request.ContentType = "application/json";
            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(jsonOutput);
            }

            var response = (HttpWebResponse)request.GetResponse();
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                // TODO: add custom exceptions
                throw new Exception();
            }
        }
    }
}
