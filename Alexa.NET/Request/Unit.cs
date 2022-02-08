using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Unit
    {
        [JsonProperty("unitId")]
        public string UnitID { get; set; }

        [JsonProperty("persistentUnitId")]
        public string PersistentUnitID { get; set; }
    }
}