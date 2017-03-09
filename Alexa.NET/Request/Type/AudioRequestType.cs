using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public enum AudioRequestType
    {
        [EnumMember(Value = "PlaybackStarted")]
        PlaybackStarted,
        [EnumMember(Value = "PlaybackFinished")]
        PlaybackFinished,
        [EnumMember(Value = "PlaybackStopped")]
        PlaybackStopped,
        [EnumMember(Value = "PlaybackNearlyFinished")]
        PlaybackNearlyFinished,
        [EnumMember(Value = "PlaybackFailed")]
        PlaybackFailed,
        [EnumMember(Value = "Unknown")]
        Unknown
    }
}
