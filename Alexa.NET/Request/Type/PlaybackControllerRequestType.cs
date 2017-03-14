using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public enum PlaybackControllerRequestType
    {
        [EnumMember(Value = "NextCommandIssued")]
        Next,
        [EnumMember(Value = "PauseCommandIssued")]
        Pause,
        [EnumMember(Value = "PlayCommandIssued")]
        Play,
        [EnumMember(Value = "PreviousCommandIssued")]
        Previous,
        [EnumMember(Value = "Unknown")]
        Unknown
    }
}
