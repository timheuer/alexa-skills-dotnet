using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPCustomSchemeLink : ISendToPhoneLink
    {
        [JsonProperty("appIdentifier")]
        public string AppIdentifier { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}