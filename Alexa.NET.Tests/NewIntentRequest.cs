using Newtonsoft.Json;

namespace Alexa.NET.Tests
{
    public class NewIntentRequest : Request.Type.Request
    {
        [JsonProperty("testProperty")]
        public bool TestProperty { get; set; }
    }
}