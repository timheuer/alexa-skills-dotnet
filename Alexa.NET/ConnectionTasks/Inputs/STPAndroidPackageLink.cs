using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPAndroidPackageLink : ISendToPhoneLink
    {
        [JsonProperty("packageIdentifier")]
        public string PackageIdentifier { get; set; }
    }
}