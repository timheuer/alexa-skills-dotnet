using System.Runtime.Serialization;

namespace Alexa.NET.Response.Directive
{
    public enum OnCompleteAction
    {
        [EnumMember(Value="RESUME_SESSION")]
        ResumeSession,
        [EnumMember(Value="SEND_ERRORS_ONLY")]
        SendErrorsOnly
    }
}