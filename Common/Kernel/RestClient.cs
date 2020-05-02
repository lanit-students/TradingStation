using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

using DTO;
using Kernel.CustomExceptions;
using Kernel.Enums;

namespace Kernel
{
    /// <summary>
    /// Wrapper of http web requests and responses that encapsulate GET, PUT POST and DELETE requests
    /// </summary>
    public class RestClient<TIn, TOut>
        where TIn : class
    {
        private readonly HttpWebRequest _request;

        private BaseException GetCustomException(HttpStatusCode statusCode) =>
            statusCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestException(),
                HttpStatusCode.NotFound => new NotFoundException(),
                HttpStatusCode.Forbidden => new ForbiddenException(),
                _ => new InternalServerException()
            };

        private async Task<TOut> GetResponseAsync()
        {
            try
            {
                var response = (HttpWebResponse)await _request.GetResponseAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    throw GetCustomException(response.StatusCode);

                using var responseStream = response.GetResponseStream();
                using var streamReader = new StreamReader(responseStream, Encoding.UTF8);

                return JsonSerializer.Deserialize<TOut>(
                    streamReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw new InternalServerException();
                }

                throw GetCustomException(((HttpWebResponse)e.Response).StatusCode);
            }
        }

        private TOut GetResponse()
        {
            try
            {
                var response = (HttpWebResponse)_request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                    throw GetCustomException(response.StatusCode);

                using var responseStream = response.GetResponseStream();
                using var streamReader = new StreamReader(responseStream, Encoding.UTF8);

                return JsonSerializer.Deserialize<TOut>(
                    streamReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw new InternalServerException();
                }

                throw GetCustomException(((HttpWebResponse)e.Response).StatusCode);
            }
        }

        private void WriteRequestBody(TIn bodyObject)
        {
            if (bodyObject == null)
                return;

            using var streamWriter = new StreamWriter(_request.GetRequestStream());

            streamWriter.Write(JsonSerializer.Serialize(bodyObject));
        }

        public RestClient(
            string url,
            RestRequestType requestType,
            UserToken token = null,
            string contentType = "application/json",
            Dictionary<string, string> queryParams = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new BadRequestException("Request URL is empty.");

            if (token != null && (token.UserId == Guid.Empty || string.IsNullOrEmpty(token.Body)))
                throw new BadRequestException("Token is empty.");

            var uriBuilder = new UriBuilder(url.TrimEnd('/'));

            if (queryParams != null)
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                foreach (var param in queryParams)
                {
                    query[param.Key] = param.Value;
                }

                uriBuilder.Query = query.ToString();
            }

            _request = WebRequest.CreateHttp(uriBuilder.Uri);
            _request.ContentType = contentType;
            _request.Method = requestType.ToString();

            if (token != null)
            {
                _request.Headers.Add("userId", token.UserId.ToString());
                _request.Headers.Add("token", token.Body);
            }
        }

        public async Task<TOut> ExecuteAsync(TIn bodyObject = null)
        {
            WriteRequestBody(bodyObject);

            var res = await GetResponseAsync();

            return res;
        }

        public TOut Execute(TIn bodyObject = null)
        {
            WriteRequestBody(bodyObject);

            return GetResponse();
        }
    }
}
