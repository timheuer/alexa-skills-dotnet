using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    public class SmartHomeRequest
    {
        [JsonProperty("directive")]
        public SmartHomeDirective Directive { get; set; }
    }
}
