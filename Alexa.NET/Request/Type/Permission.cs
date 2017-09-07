using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class Permission
    {
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}