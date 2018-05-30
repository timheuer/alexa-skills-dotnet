using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class CanFulfillIntentRequest:Request
    {
        [JsonProperty("dialogState")]
        public string DialogState { get; set; }

        [JsonProperty("intent")]
        public Intent Intent { get; set; }
    }
}
