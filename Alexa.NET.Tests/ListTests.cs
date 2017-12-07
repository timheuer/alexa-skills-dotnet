using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.ListManagement;
using Newtonsoft.Json.Linq;
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
            var managementDomain = new Uri(ListManagementClient.ListManagementDomain, UriKind.Absolute);
            Assert.Equal(managementDomain.Host, lists.Client.BaseAddress.Host);
        }

        [Fact]
        public async Task CallAddsCorrectHeaders()
        {
            var client = CreateListManagementClient(req =>
            {
                Assert.Equal("Bearer", req.Headers.Authorization.Scheme);
                Assert.Equal(DefaultAuthToken, req.Headers.Authorization.Parameter);
                return new HttpResponseMessage(HttpStatusCode.OK);
            });

            await client.GetLists();
        }

        [Fact]
        public async Task GetListsGeneratesCorrectRequest()
        {
            var client = CreateListManagementClient(req =>
            {
                Assert.Equal(HttpMethod.Get, req.Method);
                Assert.Equal(req.RequestUri.AbsolutePath, "/v2/householdlists/");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}")
                };
            });

            await client.GetLists();
        }

        [Fact]
        public void GetListsResponseGeneratesContent()
        {
            var response = new GetListResponse
            {
                Lists = new List<SkillListMetadata>
                {
                    new SkillListMetadata
                    {
                        ListId = "shopping_list_list_id",
                        Name = "Alexa shopping list",
                        Version=1,
                        State = SkillListState.Active,
                        StatusMap = new Dictionary<string,string>
                        {
                            {"active","url"},
                            {"completed","url"}
                        }
                    },
                    new SkillListMetadata
                    {
                        ListId = "todo_list_list_id",
                        Name = "Alexa to-do list",
                        Version=1,
                        State = SkillListState.Active,
                        StatusMap = new Dictionary<string,string>
                        {
                            {"active","url"},
                            {"completed","url"}
                        }
                    },
                    new SkillListMetadata
                    {
                        ListId = "ff097d45-c098-44af-a2e9-7dae032b270b",
                        Name = "test-list-name-veba",
                        Version=7,
                        State = SkillListState.Active,
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

        [Fact]
        public async Task GetListGeneratesCorrectRequest()
        {
            var client = CreateListManagementClient(req =>
            {
                Assert.Equal(HttpMethod.Get, req.Method);
                Assert.Equal(req.RequestUri.AbsolutePath, "/v2/householdlists/testid/active");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}")
                };
            });

            await client.GetList("testid", SkillListState.Active);
        }

        [Fact]
        public void FullListGeneratesContent()
        {
            var list = new SkillList
            {
                ListId = "test",
                Name = "name",
                State = SkillListState.Active,
                Version = 7,
                Items = new List<SkillListItem>
                {new SkillListItem{
                    Id = "testitem",
                    Version=3,
                    Value="test",
                    Status=SkillListItemStatus.Completed,
                    CreatedTime = new DateTime(2017,07,19,23,24,10,DateTimeKind.Utc),
                    UpdatedTime = new DateTime(2017,07,19,23,24,10,DateTimeKind.Utc),
                    Href="url"
                    }
                },
                Links = new Dictionary<string, string>
                {
                    {"next", "v2/householdlists/{listId}/{status}?nextToken={nextToken}"}
                }
            };
            Assert.True(Utility.CompareJson(list, "ListSingleList.json"));
        }

        [Fact]
        public async Task GetItemGeneratesCorrectRequest()
        {
            var client = CreateListManagementClient(req =>
            {
                Assert.Equal(HttpMethod.Get, req.Method);
                Assert.Equal(req.RequestUri.AbsolutePath, "/v2/householdlists/list/items/item");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}")
                };
            });

            await client.GetItem("list", "item");
        }


        private ListManagementClient CreateListManagementClient(Func<HttpRequestMessage, HttpResponseMessage> handlerAction)
        {
            var handler = new ActionMessageHandler(handlerAction);
            var client = new HttpClient(handler);
            return new ListManagementClient(DefaultAuthToken, client);
        }
    }
}
