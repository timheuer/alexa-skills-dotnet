using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alexa.NET.Response.Directive
{
    public enum ClearBehavior
    {
        [EnumMember(Value = "CLEAR_ENQUEUED")]
        ClearEnqueued,
        [EnumMember(Value = "CLEAR_ALL")]
        ClearAll
    }
}
