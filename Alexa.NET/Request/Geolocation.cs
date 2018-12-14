using System;
using Alexa.NET.Request.Type;
using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Geolocation
    {
        [JsonProperty("locationServices", NullValueHandling = NullValueHandling.Ignore)]
        public LocationServices LocationServices { get; set; }
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }
        [JsonProperty("coordinate", NullValueHandling = NullValueHandling.Ignore)]
        public GeolocationCoordinate Coordinate { get; set; }
        [JsonProperty("altitude",NullValueHandling = NullValueHandling.Ignore)]
        public GeolocationAltitude Altitude { get; set; }
        [JsonProperty("heading", NullValueHandling = NullValueHandling.Ignore)]
        public GeolocationHeading Heading { get; set; }
        [JsonProperty("speed", NullValueHandling = NullValueHandling.Ignore)]
        public GeolocationSpeed Speed { get; set; }
    }
}