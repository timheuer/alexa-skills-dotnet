using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alexa.NET.Response.Directive
{
    public enum PlayBehavior
    {
        [EnumMember(Value = "REPLACE_ALL")]
        ReplaceAll,
        [EnumMember(Value = "ENQUEUE")]
        Enqueue,
        [EnumMember(Value = "REPLACE_ENQUEUED")]
        ReplaceEnqueued
    }
}
