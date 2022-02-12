using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPAndroidCustomIntentLink : ISendToPhoneLink
    {
        [JsonProperty("appIdentifier")]
        public string AppIdentifier { get; set; }

        [JsonProperty("intentSchemeUri")]
        public string IntentSchemeUri { get; set; }
    }
}