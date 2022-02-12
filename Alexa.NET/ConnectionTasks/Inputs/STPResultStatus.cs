using System.Runtime.Serialization;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public enum STPResultStatus
    {
        [EnumMember(Value="SUCCESS")]
        Success,
        [EnumMember(Value="FAILURE")]
        Failure
    }
}