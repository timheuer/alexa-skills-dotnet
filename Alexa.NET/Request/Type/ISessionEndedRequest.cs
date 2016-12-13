using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public interface ISessionEndedRequest : IRequest
    {
        [JsonProperty("reason")]
        string Reason { get; set; }
    }
}