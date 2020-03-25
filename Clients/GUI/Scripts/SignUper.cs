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
        public static HttpStatusCode SignUp(SignUpData insert)
        {
            UserEmailPassword output = new UserEmailPassword(insert.Email, insert.Password);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5002/authentication/login");
            request.Method = "POST";
            string json = JsonSerializer.Serialize(insert);
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
