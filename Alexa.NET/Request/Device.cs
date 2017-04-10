using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request
{
    public class Device
    {
        [JsonProperty("deviceId")]
        public string DeviceID { get; set; }

        [JsonProperty("supportedInterfaces")]
        public Dictionary<string, object> SupportedInterfaces { get; set; }

        public bool IsInterfaceSupported(string interfaceName)
        {
            var hasInterface = SupportedInterfaces?.ContainsKey(interfaceName);
            return (hasInterface.HasValue ? hasInterface.Value : false); 
        }
    }
}
