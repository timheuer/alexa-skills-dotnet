using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{
    public class AudioPlayerRequest: Request
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("offsetInMilliseconds")]
        public long OffsetInMilliseconds { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("currentPlaybackState")]
        public PlaybackState CurrentPlaybackState { get; set; }

        [JsonProperty("enqueuedToken")]
        public string EnqueuedToken { get; set; }
        
        public bool HasEnqueuedItem
        {
            get
            {
                /*if (EnqueuedToken != null && EnqueuedToken != "")
                    return true;
                else
                    return false;
                    */
                return !String.IsNullOrEmpty(EnqueuedToken);
            }
        }

        public AudioRequestType AudioRequestType
        {
            get
            {
                switch (this.Type.Split('.')[1])
                {
                    case "PlaybackStarted":
                        return AudioRequestType.PlaybackStarted;
                    case "PlaybackFinished":
                        return AudioRequestType.PlaybackFinished;
                    case "PlaybackStopped":
                        return AudioRequestType.PlaybackStopped;
                    case "PlaybackNearlyFinished":
                        return AudioRequestType.PlaybackNearlyFinished;
                    case "PlaybackFailed":
                        return AudioRequestType.PlaybackFailed;
                    default:
                        return AudioRequestType.Unknown;
                }
            }
        }
    }
}
