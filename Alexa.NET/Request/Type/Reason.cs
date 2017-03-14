using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public enum Reason
    {
        [EnumMember(Value = "USER_INITATED")]
        UserInitiated,
        [EnumMember(Value = "ERROR")]
        Error,
        [EnumMember(Value = "EXCEEDED_MAX_REPROMPTS")]
        ExceededMaxReprompts
    }
}
