using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class GeolocationSpeed
    {
        [JsonProperty("speedInMetersPerSecond")]
        public double? Speed { get; set; }
        [JsonProperty("accuracyInMetresPerSecond")]
        public double? Accuracy { get; set; }
    }
}