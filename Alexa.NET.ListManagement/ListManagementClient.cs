using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.ListManagement;
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


        public async Task<GetListResponse> GetLists()
        {
            var response = await Client.GetStreamAsync(ListEndpoint).ConfigureAwait(false);
            using (var reader = new JsonTextReader(new StreamReader(response)))
            {
                return Serializer.Deserialize<GetListResponse>(reader);
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
    }
}
