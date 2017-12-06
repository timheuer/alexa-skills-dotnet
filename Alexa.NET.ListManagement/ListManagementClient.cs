using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.ListManagement;
using Newtonsoft.Json;

namespace Alexa.NET
{
    public class ListManagementClient
    {
        public const string ListManagementDomain = "https://api.amazonalexa.com";
        public HttpClient Client { get; }

        private const string GetListsEndpoint = "v2/householdlists/";

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
            var response = await Client.GetStreamAsync(GetListsEndpoint);
            using (var reader = new JsonTextReader(new StreamReader(response)))
            {
                return Serializer.Deserialize<GetListResponse>(reader);
            }
        }
    }
}
