using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    /// <summary>
    /// Payload for BrightnessController AdjustBrightness directive
    /// </summary>
    public class BrightnessControllerAdjustPayload
    {
        [JsonProperty("brightnessDelta")]
        public int BrightnessDelta { get; set; }
    }
}
