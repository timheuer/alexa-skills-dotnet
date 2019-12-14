using Newtonsoft.Json.Linq;

namespace Alexa.NET.Request.Type
{
    //https://developer.amazon.com/en-US/docs/alexa/smapi/voice-permissions-for-reminders.html#send-a-connectionssendrequest-directive
    public class AskForRequestHandler : IConnectionResponseHandler
    {
        public bool CanCreate(JObject data)
        {
            return data.Value<string>("name") == "AskFor";
        }

        public ConnectionResponseRequest Create(JObject data)
        {
            return new AskForPermissionRequest();
        }
    }
}