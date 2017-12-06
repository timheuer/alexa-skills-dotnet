using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.ListManagement;
using Xunit;

namespace Alexa.NET.Tests
{
    public class ListTests
    {
        private const string DefaultAuthToken = "authToken";

        [Fact]
        public void ListManagementDomainSetByDefault()
        {
            var lists = new ListManagementClient(string.Empty);
            var managementDomain = new Uri(ListManagementClient.ListManagementDomain,UriKind.Absolute);
            Assert.Equal(managementDomain.Host, lists.Client.BaseAddress.Host);
        }

        [Fact]
        public async Task CallAddsCorrectHeaders()
        {
            var client = CreateListManagementClient(req =>
            {
                Assert.Equal("Bearer",req.Headers.Authorization.Scheme);
                Assert.Equal(DefaultAuthToken, req.Headers.Authorization.Parameter);
                return new HttpResponseMessage(HttpStatusCode.OK);
            });

            await client.GetLists();
        }

        [Fact]
        public async Task GetListGeneratesCorrectRequest()
        {
            var client = CreateListManagementClient(req =>
            {
                Assert.Equal(HttpMethod.Get, req.Method);
                Assert.Equal(req.RequestUri.AbsolutePath,"/v2/householdlists/");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}")
                };
            });

            await client.GetLists();
        }

        [Fact]
        public void GetListResponseGeneratesContent()
        {
            var response = new GetListResponse
            {
                Lists = new List<ListMetadata>
                {
                    new ListMetadata
                    {
                        ListId = "shopping_list_list_id",
                        Name = "Alexa shopping list",
                        Version=1,
                        State = ListState.Active,
                        StatusMap = new Dictionary<string,string>
                        {
                            {"active","url"},
                            {"completed","url"}
                        }
                    },
                    new ListMetadata
                    {
                        ListId = "todo_list_list_id",
                        Name = "Alexa to-do list",
                        Version=1,
                        State = ListState.Active,
                        StatusMap = new Dictionary<string,string>
                        {
                            {"active","url"},
                            {"completed","url"}
                        }
                    },
                    new ListMetadata
                    {
                        ListId = "ff097d45-c098-44af-a2e9-7dae032b270b",
                        Name = "test-list-name-veba",
                        Version=7,
                        State = ListState.Active,
                        StatusMap = new Dictionary<string,string>
                        {
                            {"active","url"},
                            {"completed","url"}
                        }
                    }
                }
            };


            Utility.CompareJson(response, "ListGetListResponse.json");
        }


        private ListManagementClient CreateListManagementClient(Func<HttpRequestMessage, HttpResponseMessage> handlerAction)
        {
            var handler = new ActionMessageHandler(handlerAction);
            var client = new HttpClient(handler);
            return new ListManagementClient(DefaultAuthToken,client);
        }
    }
}
