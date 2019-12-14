using Newtonsoft.Json;

namespace Alexa.NET
{
    public class ConnectionStatus
    {
        public ConnectionStatus() { }

        public ConnectionStatus(int code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}