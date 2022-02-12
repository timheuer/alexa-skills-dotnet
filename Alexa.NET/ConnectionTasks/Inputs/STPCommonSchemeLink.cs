using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPCommonSchemeLink : ISendToPhoneLink
    {
        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}