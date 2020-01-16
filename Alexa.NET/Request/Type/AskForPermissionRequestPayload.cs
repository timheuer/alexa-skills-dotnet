using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Request.Type
{
    public class AskForPermissionRequestPayload
    {
        [JsonProperty("permissionScope")]
        public string PermissionScope { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public PermissionStatus Status { get; set; }
    }

    public enum PermissionStatus
    {
        [EnumMember(Value = "ACCEPTED")]
        Accepted,
        [EnumMember(Value = "DENIED")]
        Denied,
        [EnumMember(Value = "NOT_ANSWERED")]
        NotAccepted
    }
}