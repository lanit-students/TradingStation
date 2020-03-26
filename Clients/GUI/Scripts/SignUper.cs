using GUI.GUIModels;
using System.Net;
using DTO;
using System.IO;
using System.Text.Json;

namespace GUI.Scripts
{
    public class SignUper
    {
        public static HttpStatusCode SignUp(SignUpViewModel insert)
        {
            var output = new UserEmailPassword(insert.Email, insert.Password);

            var request = (HttpWebRequest)WebRequest.Create("https://localhost:5011/user/create");
            request.Method = "POST";
            string json = JsonSerializer.Serialize(output);
            request.ContentType = "application/json";
            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }
    }
}
