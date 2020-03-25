using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HttpWebRequestWrapperLib
{
    public class HttpWebRequestWrapper
    {
        public HttpWebRequest httpWebRequest { get; set; }
        public HttpWebResponse httpWebResponse { get; set; }

        public HttpWebRequestWrapper(string contentType = "application/json")
        {
            httpWebRequest.ContentType = contentType;
        }
        
        public string Get(string url, Dictionary<string,string> dictionary)
        {
            if (dictionary != null || dictionary.Count < 1)
            {
                url = getUrlWithParams(url, dictionary);
            }
            httpWebRequest.Method = "GET";
            HttpWebRequest.Create(url);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Put(string url, Dictionary<string, string> dictionary, string json)
        {
            if (dictionary != null || dictionary.Count < 1)
            {
                url = getUrlWithParams(url, dictionary);
            }

            httpWebRequest.Method = "PUT";
            HttpWebRequest.Create(url);

            using Stream requestStream = httpWebRequest.GetRequestStream();
            using StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
            streamWriter.WriteLine(json);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Post(string url, Dictionary<string, string> dictionary, string json)
        {
            if (dictionary != null || dictionary.Count < 1)
            {
                url = getUrlWithParams(url, dictionary);
            }

            httpWebRequest.Method = "POST";
            HttpWebRequest.Create(url);

            using Stream requestStream = httpWebRequest.GetRequestStream();
            using StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
            streamWriter.WriteLine(json);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Delete(string url, Dictionary<string, string> dictionary)
        {
            if (dictionary != null || dictionary.Count < 1)
            {
                url = getUrlWithParams(url, dictionary);
            }

            httpWebRequest.Method = "DELETE";
            HttpWebRequest.Create(url);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        private string getUrlWithParams(string url, Dictionary<string, string> dictionary)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(url + "?");
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                sb.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
