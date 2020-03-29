using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

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
            var httpWebRequest = getHttpWebRequest(url, queryParams, headerCollection, cookieContainer);
            httpWebRequest.Method = "GET";

            var result = getResultFromRequest(httpWebRequest);

            return result;
        }

        public string Put(string url, Dictionary<string, string> queryParams = null,
           object body = null, Dictionary<string, string> headerCollection = null,
           Dictionary<string, string> cookieContainer = null)
        {
            var httpWebRequest = getHttpWebRequest(url, queryParams, headerCollection, cookieContainer);
            httpWebRequest.Method = "PUT";

            if (!(body is null))
            {
                using var requestStream = httpWebRequest.GetRequestStream();
                using var streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
                string jsonBody = JsonSerializer.Serialize(body);
                streamWriter.WriteLine(jsonBody);
            }
            var result = getResultFromRequest(httpWebRequest);

            return result;
        }

        public string Post(string url, Dictionary<string, string> queryParams = null,
            object body = null, Dictionary<string, string> headerCollection = null,
            Dictionary<string, string> cookieContainer = null)
        {
            var httpWebRequest = getHttpWebRequest(url, queryParams, headerCollection, cookieContainer);
            httpWebRequest.Method = "POST";

            if (!(body is null))
            {
                using var requestStream = httpWebRequest.GetRequestStream();
                using var streamWriter = new StreamWriter(requestStream, Encoding.UTF8);
                string jsonBody = JsonSerializer.Serialize(body);
                streamWriter.WriteLine(jsonBody);
            }
            var result = getResultFromRequest(httpWebRequest);

            return result;
        }

        public string Delete(string url, Dictionary<string, string> queryParams = null,
             Dictionary<string, string> headerCollection = null, Dictionary<string, string> cookieContainer = null)
        {
            var httpWebRequest = getHttpWebRequest(url, queryParams, headerCollection, cookieContainer);
            httpWebRequest.Method = "DELETE";

            string result = getResultFromRequest(httpWebRequest);

            return result;
        }

        private HttpWebRequest getHttpWebRequest(string url, Dictionary<string, string> queryParams = null,
            Dictionary<string, string> headerCollection = null, Dictionary<string, string> cookieContainer = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new NullReferenceException();
            }
            url = getUrlWithParams(url, queryParams);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers = getHeaderCollectionFromDictionary(headerCollection);
            httpWebRequest.CookieContainer = getCookieContainerFromDictionary(cookieContainer);
            if (httpWebRequest.ContentType is null)
            {
                httpWebRequest.ContentType = ContentType;
            }
            return httpWebRequest;
        }

        private string getResultFromRequest(HttpWebRequest httpWebRequest)
        {
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            // TODO: Handling custom exceptions
            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Response returned with error");
            }
            using var responseStream = httpWebResponse.GetResponseStream();
            using var streamReader = new StreamReader(responseStream, Encoding.UTF8);
            return streamReader.ReadToEnd();
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
            if (!(dictionary is null || dictionary.Count < 1))
            {
                foreach (var keyValuePair in dictionary)
                {
                    headerCollection.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            return headerCollection;
        }

        private CookieContainer getCookieContainerFromDictionary(Dictionary<string, string> dictionary)
        {
            var cookieContainer = new CookieContainer();
            if (!(dictionary is null || dictionary.Count < 1))
            {
                foreach (var keyValuePair in dictionary)
                {
                    cookieContainer.Add(new Cookie(keyValuePair.Key, keyValuePair.Value));
                }
            }
            return cookieContainer;
        }
    }
}
