using System;
using System.Collections.Generic;
using System.Linq;
using Alexa.NET.ConnectionTasks;
using Alexa.NET.ConnectionTasks.Inputs;
using Alexa.NET.Request.Type;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response.Converters
{
    public class ConnectionTaskConverter : JsonConverter
    {
        public static Dictionary<string, Func<IConnectionTask>> TaskFactoryFromUri = new Dictionary<string, Func<IConnectionTask>>
        {
            {"PrintPDFRequest/1",() => new PrintPdfV1() },
            {"PrintImageRequest/1", () => new PrintImageV1() },
            {"PrintWebPageRequest/1",() => new PrintWebPageV1()},
            {"ScheduleTaxiReservationRequest/1",() => new ScheduleTaxiReservation() },
            {"ScheduleFoodEstablishmentReservationRequest/1",() => new ScheduleFoodEstablishmentReservation()}
        };

        public static readonly List<IConnectionTaskConverter> ConnectionTaskConverters = new List<IConnectionTaskConverter>();

        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            IConnectionTask directive;

            // Check task factories
            var typeKey = jsonObject.Value<string>("@type");
            var versionKey = jsonObject.Value<string>("@version");
            var factoryKey = $"{typeKey}/{versionKey}";
            var hasFactory = TaskFactoryFromUri.ContainsKey(factoryKey);
            if(hasFactory)
            {
                directive = TaskFactoryFromUri[factoryKey]();
            }
            else
            {
                // Check task converters
                var converter = ConnectionTaskConverters.FirstOrDefault(c => c.CanConvert(jsonObject));
                if(converter is null) 
                    throw new Exception(
                         $"unable to deserialize response. " +
                         $"unrecognized task type '{typeKey}' with version '{versionKey}'"
                     );
                directive = converter.Convert(jsonObject);
            }
                

            serializer.Populate(jsonObject.CreateReader(), directive);

            return directive;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IConnectionTask);
        }
    }
}