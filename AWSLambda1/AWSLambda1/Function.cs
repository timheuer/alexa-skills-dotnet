using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            IOutputSpeech innerResponse = null;
            var log = context.Logger;

            if (input.GetRequestType() == typeof(ILaunchRequest))
            {
                // default launch request, let's just let them know what you can do
                log.LogLine($"Default LaunchRequest made");

                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = "Welcome to number functions.  You can ask us to add numbers!";
            }
            else if (input.GetRequestType() == typeof(IIntentRequest))
            {
                // intent request, process the intent
                log.LogLine($"Intent Requested {input.Request.Intent.Name}");

                // AddNumbersIntent
                // get the slots
                var n1 = Convert.ToDouble(input.Request.Intent.Slots["firstnum"].Value);
                var n2 = Convert.ToDouble(input.Request.Intent.Slots["secondnum"].Value);

                double result = n1 + n2;

                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = $"The result is {result.ToString()}.";

            }

            var rep = new Reprompt();
            var repspeech = new PlainTextOutputSpeech() { Text = "Are you listenitng to me?" };
            rep.OutputSpeech = repspeech;

            var response = ResponseBuilder.Ask(innerResponse, rep);
            return response;
        }
    }
}