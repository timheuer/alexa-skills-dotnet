using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response.Directive
{
    public interface IConnectionSendRequestHandler
    {
        bool CanCreate(JObject data);

        ConnectionSendRequest Create();
    }
}