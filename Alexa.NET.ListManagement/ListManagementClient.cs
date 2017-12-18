using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.ListManagement;
using Alexa.NET.ListManagement.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET
{
    public class ListManagementClient
    {
        public const string ListManagementDomain = "https://api.amazonalexa.com";
        public HttpClient Client { get; }

        private const string ListEndpoint = "v2/householdlists/";

        private static readonly JsonSerializer Serializer = JsonSerializer.CreateDefault();

        public ListManagementClient(string token) : this(token,new HttpClient())
        {
        }

        public ListManagementClient(string token, HttpClient client)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(ListManagementDomain, UriKind.Absolute);
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            Client = client;
        }

        public async Task<List<SkillListMetadata>> GetListsMetadata()
        {
            var response = await Client.GetStreamAsync(ListEndpoint).ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(response)))
            {
                return Serializer.Deserialize<GetListResponse>(reader)?.Lists ?? new List<SkillListMetadata>();
            }
        }

        public async Task<SkillList> GetList(string listId, string listItemStatus)
        {
            var response = await Client.GetStreamAsync($"{ListEndpoint}{listId}/{listItemStatus}").ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(response)))
            {
                return Serializer.Deserialize<SkillList>(reader);
            }
        }

        public async Task<SkillListItem> GetItem(string listId, string itemId)
        {
            var response = await Client.GetStreamAsync($"{ListEndpoint}{listId}/items/{itemId}").ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(response)))
            {
                return Serializer.Deserialize<SkillListItem>(reader);
            }
        }

        public async Task<SkillListItem> AddItem(string listId, string value, string status)
        {
            var inputObject = new SkillListItemCreateRequest {Status = status, Value = value};
            var inputContent = new StringContent(JObject.FromObject(inputObject).ToString(), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"{ListEndpoint}{listId}/items",inputContent).ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false))))
            {
                return Serializer.Deserialize<SkillListItem>(reader);
            }
        }

        public async Task<SkillListItem> UpdateItem(string listId, string itemId, string value, string status, int currentItemVersion)
        {
            var inputObject = new SkillListItemUpdateRequest { Status = status, Value = value,Version=currentItemVersion };
            var inputContent = new StringContent(JObject.FromObject(inputObject).ToString(), Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{ListEndpoint}{listId}/items/{itemId}", inputContent).ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false))))
            {
                return Serializer.Deserialize<SkillListItem>(reader);
            }
        }

        public Task DeleteItem(string listId, string itemId)
        {
            return Client.DeleteAsync($"{ListEndpoint}{listId}/items/{itemId}");
        }

        public async Task<SkillListMetadata> AddList(string name)
        {
            var inputObject = new SkillListCreateRequest { Name = name, State = SkillListState.Active };
            var inputContent = new StringContent(JObject.FromObject(inputObject).ToString(), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"{ListEndpoint}", inputContent).ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false))))
            {
                return Serializer.Deserialize<SkillListMetadata>(reader);
            }
        }

        public async Task<SkillListMetadata> UpdateList(string listId, string name, string state, int currentListVersion)
        {
            var inputObject = new SkillListUpdateRequest { Name = name, State = state,Version=currentListVersion };
            var inputContent = new StringContent(JObject.FromObject(inputObject).ToString(), Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{ListEndpoint}{listId}", inputContent).ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false))))
            {
                return Serializer.Deserialize<SkillListMetadata>(reader);
            }
        }

        public Task DeleteList(string listId)
        {
            return Client.DeleteAsync($"{ListEndpoint}{listId}");
        }
    }
}
