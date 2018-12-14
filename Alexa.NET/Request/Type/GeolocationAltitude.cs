using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class GeolocationAltitude
    {
        [JsonProperty("altitudeInMeters", NullValueHandling = NullValueHandling.Ignore)]
        public double? Altitude { get; set; }

        [JsonProperty("accuracyInMeters",NullValueHandling = NullValueHandling.Ignore)]
        public double? Accuracy { get; set; }
    }
}