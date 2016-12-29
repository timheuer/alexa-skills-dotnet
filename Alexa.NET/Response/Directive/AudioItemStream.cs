using Newtonsoft.Json;

namespace Alexa.NET.Response.Directive
{
    public class AudioItemStream
    {
        [JsonRequired]
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonRequired]
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonRequired]
        [JsonProperty("expectedPreviousToken")]
        public string ExpectedPreviousToken { get; set; }

        [JsonRequired]
        [JsonProperty("offsetInMilliseconds")]
        public int OffsetInMilliseconds { get; set; }
    }
}
