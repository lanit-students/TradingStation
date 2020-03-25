using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpWebRequestWrapperLib
{
    public class HttpWebRequestWrapper
    {
        public HttpWebRequest httpWebRequest { get; set; }
        public HttpWebResponse httpWebResponse { get; set; }

        public HttpWebRequestWrapper(string url, string contentType = "application/json")
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
        }

        public string Get(string json)
        {
            httpWebRequest.Method = "GET";
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(requestStream))
                {
                    streamWriter.WriteLine(json);
                }
            }
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string result;
            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }

        public string Put()
        {
            throw new NotImplementedException();
        }

        public string Post()
        {
            throw new NotImplementedException();
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }


    }
}
