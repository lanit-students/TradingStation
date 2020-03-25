using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HttpWebRequestWrapperLib
{
    /// <summary>
    /// Wrapper of http web requests and responses that encapsulate GET, PUT POST and DELETE requests
    /// </summary>
    public class HttpWebRequestWrapper
    {
        public HttpWebRequest httpWebRequest { get; set; }
        public HttpWebResponse httpWebResponse { get; set; }

        public HttpWebRequestWrapper(string contentType = "application/json")
        {
            httpWebRequest.ContentType = contentType;
        }
        
        public string Get(string url, Dictionary<string,string> parameters)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "Url is null or empty";
            }
            if (!(parameters is null) && parameters.Count > 0)
            {
                url = getUrlWithParams(url, parameters);
            }
            httpWebRequest.Method = "GET";
            HttpWebRequest.Create(url);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Put(string url, Dictionary<string, string> parameters, string body)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "Url is null or empty";
            }
            if (string.IsNullOrEmpty(body))
            {
                return "Data in body is null or empty";
            }
            if (!(parameters is null) && parameters.Count > 0)
            {
                url = getUrlWithParams(url, parameters);
            }

            httpWebRequest.Method = "PUT";
            HttpWebRequest.Create(url);

            using Stream requestStream = httpWebRequest.GetRequestStream();
            using StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
            streamWriter.WriteLine(body);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Post(string url, Dictionary<string, string> parameters, string body)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "Url is null or empty";
            }
            if (string.IsNullOrEmpty(body))
            {
                return "Data in body is null or empty";
            }
            if (!(parameters is null) && parameters.Count > 0)
            {
                url = getUrlWithParams(url, parameters);
            }

            httpWebRequest.Method = "POST";
            HttpWebRequest.Create(url);

            using Stream requestStream = httpWebRequest.GetRequestStream();
            using StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
            streamWriter.WriteLine(body);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Delete(string url, Dictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "Url is null or empty";
            }
            if (!(parameters is null) && parameters.Count > 0 )
            {
                url = getUrlWithParams(url, parameters);
            }

            httpWebRequest.Method = "DELETE";
            HttpWebRequest.Create(url);

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        private string getUrlWithParams(string url, Dictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(url) || parameters is null || parameters.Count < 1)
            {
                throw new NullReferenceException();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(url + "?");
            foreach (KeyValuePair<string, string> keyValuePair in parameters)
            {
                sb.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
