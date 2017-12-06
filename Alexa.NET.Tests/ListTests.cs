using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Alexa.NET.Response;
using Xunit;

namespace Alexa.NET.Tests
{
    public class ListTests
    {
        [Fact]
        public void ListManagementDomainSetByDefault()
        {
            var lists = new ListManagementClient(string.Empty);
            var managementDomain = new Uri(ListManagementClient.ListManagementDomain,UriKind.Absolute);
            Assert.Equal(lists.Client.BaseAddress.Host,managementDomain.Host);
        }

        [Fact]
        public void ListManagementClientTokenSet()
        {
            var token = "abcdef";
            var lists = new ListManagementClient(token);
            Assert.Equal(lists.AccessToken,token);
        }

        private ListManagementClient CreateListManagementClient(Func<HttpRequestMessage, HttpResponseMessage> handlerAction)
        {
            var handler = new ActionMessageHandler(handlerAction);
            var client = new HttpClient(handler);
            return new ListManagementClient("authToken",client);
        }
    }
}
