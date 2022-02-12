using System.Linq;
using Alexa.NET.Request.Type;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class SendToPhoneConverter : IConnectionTaskConverter
    {
        private static readonly JsonSerializer Serializer = JsonSerializer.Create();

        public bool CanConvert(JObject jObject)
        {
            return jObject.ContainsKey("links");
        }

        public IConnectionTask Convert(JObject jObject)
        {
            var obj = new SendToPhone();
            Serializer.Populate(jObject.CreateReader(),obj);
            return obj;
        }

        public static void AddToConnectionTaskConverters()
        {
            if (ConnectionTaskConverter.ConnectionTaskConverters.Where(rc => rc != null)
                .All(rc => rc.GetType() != typeof(SendToPhoneConverter)))
            {
                ConnectionTaskConverter.ConnectionTaskConverters.Add(new SendToPhoneConverter());
            }
        }
    }
}