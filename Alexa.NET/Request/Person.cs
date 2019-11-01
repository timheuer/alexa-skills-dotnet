using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Person
    {
        [JsonProperty("personId")]
        public string PersonId { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}