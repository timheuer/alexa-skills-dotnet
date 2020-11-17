using Alexa.NET.ConnectionTasks;
using Alexa.NET.Request.Type;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Alexa.NET.Tests.Examples
{
    public class ExampleTaskConverter : IConnectionTaskConverter
    {
        public bool CanConvert(JObject jObject)
        {
            var property = jObject["randomParameter"];
            return property is object;
        }

        public IConnectionTask Convert(JObject jObject)
        {
            return new ExampleTask();
        }

        public static void AddToConnectionTaskConverters()
        {
            if (ConnectionTaskConverter.ConnectionTaskConverters.Where(rc => rc != null)
                .All(rc => rc.GetType() != typeof(ExampleTaskConverter)))
            {
                ConnectionTaskConverter.ConnectionTaskConverters.Add(new ExampleTaskConverter());
            }
        }
    }
}
