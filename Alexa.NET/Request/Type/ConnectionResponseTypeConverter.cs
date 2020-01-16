using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Request.Type
{
    public class ConnectionResponseTypeConverter : IDataDrivenRequestTypeConverter
    {
        public static List<IConnectionResponseHandler> Handlers = new List<IConnectionResponseHandler>
        {
            new AskForRequestHandler()
        };
        public bool CanConvert(string requestType)
        {
            return requestType == "Connections.Response";
        }

        public Request Convert(string requestType)
        {
            throw new NotImplementedException();
        }

        public Request Convert(JObject data)
        {
            var handler = Handlers.FirstOrDefault(h => h.CanCreate(data));
            return handler?.Create(data);
        }
    }
}