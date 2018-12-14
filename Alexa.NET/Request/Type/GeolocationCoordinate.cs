using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class GeolocationCoordinate
    {
        [JsonProperty("latitudeInDegrees")]
        public double Latitude { get; set; }

        [JsonProperty("longitudeInDegrees")]
        public double Longitude { get; set; }

        [JsonProperty("accuracyInMeters")]
        public double Accuracy { get; set; }
    }
}