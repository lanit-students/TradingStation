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
    public class SignUper
    {
        public static HttpStatusCode SignUp(SignUpViewModel insert)
        {
            UserEmailPassword output = new UserEmailPassword(insert.Email, insert.Password);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5011/user/create");
            request.Method = "POST";
            string json = JsonSerializer.Serialize(output);
            request.ContentType = "application/json";
            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(json);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }
    }
}
