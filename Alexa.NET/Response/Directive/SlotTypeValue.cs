using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class SlotTypeValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public SlotTypeValueName Name { get; set; }
    }
}