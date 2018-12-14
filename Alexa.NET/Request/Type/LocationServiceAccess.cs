using System.Runtime.Serialization;

namespace Alexa.NET.Request.Type
{
    public enum LocationServiceAccess
    {
        [EnumMember(Value = "ENABLED")]
        Enabled,
        [EnumMember(Value = "DISABLED")]
        Disabled
    }
}