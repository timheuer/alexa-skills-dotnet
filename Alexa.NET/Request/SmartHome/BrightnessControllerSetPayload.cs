using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    /// <summary>
    /// Payload for BrightnessController SetBrightness directive
    /// </summary>
    public class BrightnessControllerSetPayload
    {
        [JsonProperty("brightness")]
        public int Brightness { get; set; }
    }
}
