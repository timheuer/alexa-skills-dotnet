using System.Runtime.Serialization;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public enum PinConfirmationReason
    {
        [EnumMember(Value = "METHOD_LOCKOUT")] MethodLockout,

        [EnumMember(Value = "VERIFICATION_METHOD_NOT_SETUP")]
        VerificationMethodNotSetup,
        [EnumMember(Value = "NOT_MATCH")] NotMatch
    }
}