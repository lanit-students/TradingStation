using Newtonsoft.Json;
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
        public string ContentType { get; set; }

        public HttpWebRequestWrapper()
        {
            ContentType = "application/json";
        }
        
        public string Get(string url, Dictionary<string, string> queryParams = null,
            Dictionary<string, string> headerCollection = null, Dictionary<string, string> cookieContainer = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NullReferenceException();
            }
            url = getUrlWithParams(url, queryParams);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = ContentType;
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers = getHeaderCollectionFromDictionary(headerCollection);
            httpWebRequest.CookieContainer = getCookieContainerFromDictionary(cookieContainer);

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            // TODO: Handling custom exceptions
            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Response returned with error");
            }
            using var responseStream = httpWebResponse.GetResponseStream();
            using var streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Put(string url, Dictionary<string, string> queryParams = null,
            object body = null, Dictionary<string, string> headerCollection = null,
            Dictionary<string, string> cookieContainer = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NullReferenceException();
            }
            url = getUrlWithParams(url, queryParams);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = ContentType;
            httpWebRequest.Method = "PUT";
            httpWebRequest.Headers = getHeaderCollectionFromDictionary(headerCollection);
            httpWebRequest.CookieContainer = getCookieContainerFromDictionary(cookieContainer);

            if (! (body is null))
            {
                using var requestStream = httpWebRequest.GetRequestStream();
                using var streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
                var jsonBody = JsonConvert.SerializeObject(body);
                streamWriter.WriteLine(body);
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            // TODO: Handling custom exceptions
            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Response returned with error");
            }
            using var responseStream = httpWebResponse.GetResponseStream();
            using var streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Post(string url, Dictionary<string, string> queryParams = null,
            object body = null, Dictionary<string, string> headerCollection = null,
            Dictionary<string, string> cookieContainer = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NullReferenceException();
            }
            url = getUrlWithParams(url, queryParams);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = ContentType;
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers = getHeaderCollectionFromDictionary(headerCollection);
            httpWebRequest.CookieContainer = getCookieContainerFromDictionary(cookieContainer);

            if (! (body is null))
            {
                using var requestStream = httpWebRequest.GetRequestStream();
                using var streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
                var jsonBody = JsonConvert.SerializeObject(body);
                streamWriter.WriteLine(body);
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            // TODO: Handling custom exceptions
            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Response returned with error");
            }
            using var responseStream = httpWebResponse.GetResponseStream();
            using var streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        public string Delete(string url, Dictionary<string, string> queryParams = null,
             Dictionary<string, string> headerCollection = null, Dictionary<string, string> cookieContainer = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NullReferenceException();
            }
            url = getUrlWithParams(url, queryParams);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = ContentType;
            httpWebRequest.Method = "DELETE";
            httpWebRequest.Headers = getHeaderCollectionFromDictionary(headerCollection);
            httpWebRequest.CookieContainer = getCookieContainerFromDictionary(cookieContainer);

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            // TODO: Handling custom exceptions
            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Response returned with error");
            }
            using var responseStream = httpWebResponse.GetResponseStream();
            using var streamReader = new StreamReader(responseStream, Encoding.UTF8);
            var result = streamReader.ReadToEnd();
            return result;
        }

        private string getUrlWithParams(string url, Dictionary<string, string> queryParams)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NullReferenceException();
            }
            if (queryParams is null || queryParams.Count < 1)
            {
                return url;
            }
            var sb = new StringBuilder();
            sb.Append(url + "?");
            foreach (var keyValuePair in queryParams)
            {
                sb.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private WebHeaderCollection getHeaderCollectionFromDictionary(Dictionary<string, string> dictionary)
        {
            var headerCollection = new WebHeaderCollection();
            foreach (var keyValuePair in dictionary)
            {
                headerCollection.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return headerCollection;
        }

        private CookieContainer getCookieContainerFromDictionary(Dictionary<string, string> dictionary)
        {
            var cookieContainer = new CookieContainer();
            foreach (var keyValuePair in dictionary)
            {
                cookieContainer.Add(new Cookie(keyValuePair.Key, keyValuePair.Value));
            }
            return cookieContainer;
        }
    }
}
