using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Request.Type
{

    public class BuiltInIntent
    {

        /// <summary>
        /// Purpose: 
        /// - Let the user cancel a transaction or task (but remain in the skill)
        /// - Let the user completely exit the skill
        /// 
        /// Common Utterances: cancel, never mind, forget it
        /// </summary>
        public const string Cancel = "AMAZON.CancelIntent";

        /// <summary>
        /// Purpose: 
        /// Provide help about how to use the skill.
        /// 
        /// Common Utterances: help, help me, can you help me
        /// </summary>
        public const string Help = "AMAZON.HelpIntent";

        /// <summary>
        /// Purpose: 
        /// - Let the user stop an action (but remain in the skill)
        /// - Let the user completely exit the skill
        /// 
        /// Common Utterances: stop, off, shut up
        /// </summary>
        public const string Stop = "AMAZON.StopIntent";

        public const string Yes = "AMAZON.YesIntent";

        public const string No = "AMAZON.NoIntent";

        /// <summary>
        /// Audio Intents
        /// - These intents are for use with a skill that plays audio
        /// </summary>
        /// 

        /// <summary>
        /// Purpose: 
        /// - Lets user turn on or off an audio loop
        /// - can be used without the skill invocation name (ie: "Alexa, loop on")
        /// 
        /// Common Utterances: loop off, loop on
        /// </summary>
        public const string LoopOff = "AMAZON.LoopOffIntent";

        public const string LoopOn = "AMAZON.LoopOnIntent";

        /// <summary>
        /// Purpose: 
        /// - Lets user turn on or off shuffle mode, usually for audio skills that
        ///     stream a playlist of tracks
        /// - can be used without the skill invocation name (ie: "Alexa, loop on")
        /// 
        /// Common Utterances: loop off, loop on
        /// </summary>
        public const string ShuffleOn = "AMAZON.ShuffleOnIntent";

        public const string ShuffleOff = "AMAZON.ShuffleOffIntent";

        /// <summary>
        /// Purpose: 
        /// - Let the user navigate to the next item in a list
        /// - typically used for skills that stream audio
        /// - can be used without the skill invocation name (ie: "Alexa, next")
        /// 
        /// Common Utterances: next, skip, skip forward
        /// </summary>
        public const string Next = "AMAZON.NextIntent";

        /// <summary>
        /// Purpose: 
        /// - Let the user navigate to the previous item in a list
        /// - typically used for skills that stream audio
        /// - can be used without the skill invocation name (ie: "Alexa, go back")
        /// 
        /// Common Utterances: go back, skip back, back up
        /// </summary>
        public const string Previous = "AMAZON.PreviousIntent";

        /// <summary>
        /// Purpose: 
        /// - Let the user pause the action in progress (such as a game or audio track)
        /// - MUST be implemented for skills that stream audio
        /// - can be used without the skill invocation name (ie: "Alexa, pause")
        /// 
        /// Common Utterances: pause, pause that
        /// </summary>
        public const string Pause = "AMAZON.PauseIntent";

        /// <summary>
        /// Purpose: 
        /// - Let the user navigate repeat the last action
        /// - can be used without the skill invocation name (ie: "Alexa, repeat")
        /// 
        /// Common Utterances: repeat, say that again
        /// </summary>
        public const string Repeat = "AMAZON.RepeatIntent";

        /// <summary>
        /// Purpose: 
        /// - Let the user resume or continue an action 
        /// - MUST be implemented for skills that stream audio
        /// - can be used without the skill invocation name (ie: "Alexa, resume ")
        /// 
        /// Common Utterances: resume, continue, keep going
        /// </summary>
        public const string Resume = "AMAZON.ResumeIntent";

        /// <summary>
        /// Purpose: 
        /// - Let the user to restart an action, such as restarting a game, 
        ///     transaction, or audio track.
        /// - typically used for skills that stream audio
        /// - can be used without the skill invocation name (ie: "Alexa, restart")
        /// 
        /// Common Utterances: start over, restart, start again
        /// </summary>
        public const string StartOver = "AMAZON.StartOverIntent";

        /// <summary>
		/// Purpose
		/// - Triggered when the user's spoken input does not match any of the other intents in the skill
        /// </summary>
		public const string Fallback = "AMAZON.FallbackIntent";
    }

}
