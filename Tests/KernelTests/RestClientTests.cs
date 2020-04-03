using Kernel;
using Kernel.Enums;
using NUnit.Framework;
using System.Collections.Generic;

namespace KernelTests
{
    public class RestClientTests
    {
        private const string Url = "https://google.com/test";

        [Test]
        public void CreateNewRestClientTest()
        {
            Assert.NotNull(new RestClient<object, object>(Url, RestRequestType.GET));
        }

        [Test]
        public void CreateNewRestClientWithQueryParamsTest()
        {
            var queryParams = new Dictionary<string, string>
            {
                { "param1", "value1" },
                { "param2", "value2" }
            };

            Assert.NotNull(new RestClient<object, object>(Url, RestRequestType.POST, null, default, queryParams));
        }
    }
}