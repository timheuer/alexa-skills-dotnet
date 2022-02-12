using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPAndroidCustomIntent : ISendToPhoneLink
    {
        [JsonProperty("appIdentifier")]
        public string AppIdentifier { get; set; }

        [JsonProperty("intentSchemeUri")]
        public string IntentSchemeUri { get; set; }
    }
}