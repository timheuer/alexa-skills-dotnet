using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET
{
    public class ResponseBuilder
    {
        #region Tell Responses
        public static SkillResponse Tell(IOutputSpeech speechResponse)
        {
            return BuildResponse(speechResponse, true, null, null, null);
        }
        public static SkillResponse TellWithReprompt(IOutputSpeech speechResponse, Reprompt reprompt)
        {
            return BuildResponse(speechResponse, true, null, reprompt, null);
        }
  

        public static SkillResponse Tell(IOutputSpeech speechResponse, Session sessionAttributes)
        {
            return BuildResponse(speechResponse, true, sessionAttributes, null, null);
        }

        public static SkillResponse TellWithReprompt(IOutputSpeech speechResponse, Reprompt reprompt, Session sessionAttributes)
        {
            return BuildResponse(speechResponse, true, sessionAttributes, reprompt, null);
        }

        public static SkillResponse TellWithCard(IOutputSpeech speechResponse, string title, string content)
        {
            SimpleCard card = new SimpleCard();
            card.Content = content;
            card.Title = title;

            return BuildResponse(speechResponse, true, null, null, card);
        }

        public static SkillResponse TellWithCard(IOutputSpeech speechResponse, string title, string content, Session sessionAttributes)
        {
            SimpleCard card = new SimpleCard();
            card.Content = content;
            card.Title = title;

            return BuildResponse(speechResponse, true, sessionAttributes, null, card);
        }

        public static SkillResponse TellWithLinkAccountCard(IOutputSpeech speechResponse, string title, string content)
        {
            LinkAccountCard card = new LinkAccountCard();

            return BuildResponse(speechResponse, true, null, null, card);
        }

        public static SkillResponse TellWithLinkAccountCard(IOutputSpeech speechResponse, string title, string content, Session sessionAttributes)
        {
            LinkAccountCard card = new LinkAccountCard();

            return BuildResponse(speechResponse, true, sessionAttributes, null, card);
        }
        #endregion

        #region Ask Responses
        public static SkillResponse Ask(IOutputSpeech speechResponse, Reprompt reprompt)
        {
            return BuildResponse(speechResponse, false, null, reprompt, null);
        }

        public static SkillResponse Ask(IOutputSpeech speechResponse, Reprompt reprompt, Session sessionAttributes)
        {
            return BuildResponse(speechResponse, false, sessionAttributes, reprompt, null);
        }
        #endregion

        #region AudioPlayer Response
        public static SkillResponse AudioPlayerPlay(PlayBehavior playBehavior, string url, string token)
        {
            return AudioPlayerPlay(playBehavior, url, token, 0);
        }

        public static SkillResponse AudioPlayerPlay(PlayBehavior playBehavior, string url, string token, int offsetInMilliseconds)
        {
            return AudioPlayerPlay(playBehavior, url, token, null, offsetInMilliseconds);
        }

        public static SkillResponse AudioPlayerPlay(PlayBehavior playBehavior, string url, string token, string expectedPreviousToken, int offsetInMilliseconds)
        {
            var response = BuildResponse(null, true, null, null, null);
            response.Response.Directives.Add(new AudioPlayerPlayDirective()
            {
                PlayBehavior = playBehavior,
                AudioItem = new AudioItem()
                {
                    Stream = new AudioItemStream()
                    {
                        Url = url,
                        Token = token,
                        ExpectedPreviousToken = expectedPreviousToken,
                        OffsetInMilliseconds = offsetInMilliseconds
                    }
                }
            });

            return response;
        }

        public static SkillResponse AudioPlayerStop()
        {
            var response = BuildResponse(null, true, null, null, null);
            response.Response.Directives.Add(new StopDirective());
            return response;
        }

        public static SkillResponse AudioPlayerClearQueue(ClearBehavior clearBehavior)
        {
            var response = BuildResponse(null, true, null, null, null);
            response.Response.Directives.Add(new ClearQueueDirective()
            {
                ClearBehavior = clearBehavior
            });
            return response;
        }
        #endregion

        #region Main Response Builder
        private static SkillResponse BuildResponse(IOutputSpeech outputSpeech, bool shouldEndSession, Session sessionAttributes, Reprompt reprompt, ICard card)
        {
            SkillResponse response = new Response.SkillResponse();
            response.Version = "1.0";
            if (sessionAttributes != null) response.SessionAttributes = sessionAttributes.Attributes;

            ResponseBody body = new Response.ResponseBody();
            body.ShouldEndSession = shouldEndSession;
            body.OutputSpeech = outputSpeech;

            if (reprompt != null) body.Reprompt = reprompt;
            if (card != null) body.Card = card;

            response.Response = body;

            return response;
        }
        #endregion
    }
}
