using System.Runtime.Serialization;

namespace Alexa.NET.Request
{
    public enum SlotValueType
    {
        [EnumMember(Value="Simple")]
        Simple,
        [EnumMember(Value="List")]
        List
    }
}