using System.Runtime.Serialization;

namespace Alexa.NET.Request.Type
{
    public enum LocationServiceStatus
    {
        [EnumMember(Value = "RUNNING")]
        Running,
        [EnumMember(Value = "STOPPED")]
        Stopped
    }
}