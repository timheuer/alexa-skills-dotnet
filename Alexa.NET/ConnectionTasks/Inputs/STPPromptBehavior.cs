using System.Runtime.Serialization;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public enum STPPromptBehavior
    {
        [EnumMember(Value="SPEAK")]
        Speak,
        [EnumMember(Value="SUPPRESS")]
        Suppress
    }
}