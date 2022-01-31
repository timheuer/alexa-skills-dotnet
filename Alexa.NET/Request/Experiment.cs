using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Experiment
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("assignedVariant",NullValueHandling = NullValueHandling.Ignore)]
        public ExperimentVariant AssignedVariant { get; set; }
    }
}