using GUI.GUIModels;
using System.Net;
using DTO;
using System.IO;
using System.Text.Json;

namespace GUI.Scripts
{
    public class SignUper
    {
        public static HttpStatusCode SignUp(SignUpViewModel input)
        {
            var output = new UserEmailPassword(input.Email, input.Password);

            var request = (HttpWebRequest)WebRequest.Create("https://localhost:5011/user/create");
            request.Method = "POST";
            string jsonOutput = JsonSerializer.Serialize(output);
            request.ContentType = "application/json";
            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(jsonOutput);
            }

            var response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }
    }
}
