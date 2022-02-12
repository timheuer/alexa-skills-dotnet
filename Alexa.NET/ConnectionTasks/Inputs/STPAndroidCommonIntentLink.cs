using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPAndroidCommonIntentLink : ISendToPhoneLink
    {
        [JsonProperty("intentName")]
        public string IntentName { get; set; }

        [JsonProperty("intentSchemeUri")]
        public string IntentSchemeUri { get; set; }
    }
}