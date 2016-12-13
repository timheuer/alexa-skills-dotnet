using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class LinkAccountCard : ICard
    {
        [JsonProperty("type")]
        public string Type
        {
            get { return "LinkAccount"; }
        }
    }
}