using GUI.GUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DTO;
using System.IO;
using System.Text;
using System.Text.Json;

namespace GUI.Scripts
{
    public class SignUpper
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
