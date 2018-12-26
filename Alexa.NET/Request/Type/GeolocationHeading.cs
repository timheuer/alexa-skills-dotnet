using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class GeolocationHeading
    {
        [JsonProperty("directionInDegrees",NullValueHandling = NullValueHandling.Ignore)]
        public double? Direction { get; set; }
        [JsonProperty("accuracyInDegrees", NullValueHandling = NullValueHandling.Ignore)]
        public double? Accuracy { get; set; }
    }
}