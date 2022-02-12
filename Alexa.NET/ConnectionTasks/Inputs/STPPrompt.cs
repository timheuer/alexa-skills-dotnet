using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPPrompt
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("directLaunchDefaultPromptBehavior", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public STPPromptBehavior? DirectLaunchDefaultPromptBehavior { get; set; }
    }
}