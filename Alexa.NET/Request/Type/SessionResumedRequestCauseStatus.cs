using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class SessionResumedRequestCauseStatus
    {
        public SessionResumedRequestCauseStatus() { }

        public SessionResumedRequestCauseStatus(int code, string message)
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