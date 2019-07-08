using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class Restaurant
    {
        [JsonProperty("@type")]
        public string Type => "Restaurant";

        [JsonProperty("@version")]
        public string Version => 1.ToString();

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public PostalAddress Location { get; set; }
    }
}