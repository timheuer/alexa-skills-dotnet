using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Request.Type
{
    public class SkillEventPersistenceStatus
    {
        [JsonProperty("userInformationPersistenceStatus"),
         JsonConverter(typeof(StringEnumConverter))]
        public PersistenceStatus Status { get; set; }
    }
}