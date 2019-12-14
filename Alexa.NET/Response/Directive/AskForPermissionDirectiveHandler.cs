using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response.Directive
{
    public class AskForPermissionDirectiveHandler : IConnectionSendRequestHandler
    {
        public bool CanCreate(JObject data)
        {
            return data.Value<string>("name") == "AskFor";
        }

        public ConnectionSendRequest Create()
        {
            return new AskForPermissionDirective();
        }
    }
}