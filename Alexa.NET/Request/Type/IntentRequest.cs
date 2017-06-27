using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class IntentRequest : Request
    {
        [JsonProperty("dialogState")]
        public string DialogState { get; set; }

        public Intent Intent { get; set; }
    }
}