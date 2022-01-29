using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class AuthenticationConfidenceLevelCustomPolicy
    {
        public AuthenticationConfidenceLevelCustomPolicy(){}

        public AuthenticationConfidenceLevelCustomPolicy(string policyName)
        {
            PolicyName = policyName;
        }

        [JsonProperty("policyName")]
        public string PolicyName { get; set; }
    }
}