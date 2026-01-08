using Newtonsoft.Json;

namespace Alexa.NET.Response.SmartHome
{
    public class AdditionalAttributes
    {
        [JsonProperty("manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public string Manufacturer { get; set; }

        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty("serialNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string SerialNumber { get; set; }

        [JsonProperty("firmwareVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string FirmwareVersion { get; set; }

        [JsonProperty("softwareVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string SoftwareVersion { get; set; }

        [JsonProperty("customIdentifier", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomIdentifier { get; set; }
    }
}
