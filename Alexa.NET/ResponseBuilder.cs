using Alexa.NET.Request;
using Alexa.NET.Response;
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

        public static SkillResponse Tell(IOutputSpeech speechResponse, Session sessionAttributes)
        {
            return BuildResponse(speechResponse, true, sessionAttributes, null, null);
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

        public static SkillResponse Ask(IOutputSpeech speechResponse, Reprompt reprompt)
        {
            return BuildResponse(speechResponse, false, null, reprompt, null);
        }

        public static SkillResponse Ask(IOutputSpeech speechResponse, Reprompt reprompt, Session sessionAttributes)
        {
            return BuildResponse(speechResponse, false, sessionAttributes, reprompt, null);
        }

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

            response.Response = body; //var test

            return response;
        }
    }
}
