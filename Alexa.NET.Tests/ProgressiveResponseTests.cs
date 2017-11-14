using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Tests
{
    public class ProgressiveResponseTests
    {
        [Fact]
        public void VoicePlayerCreatesCorrectJson()
        {
            var directive = new VoicePlayerSpeakDirective("This text is spoken while your skill processes the full response.");
            Assert.True(Utility.CompareJson(directive, "VoicePlayerSpeakDirective.json"));
        }

        [Fact]
        public void ProgressiveResponseRequestCreatesCorrectJson()
        {
            var header = new ProgressiveResponseHeader("amzn1.echo-api.request.xxxxxxx");
            var directive = new VoicePlayerSpeakDirective("This text is spoken while your skill processes the full response.");
            var request = new ProgressiveResponseRequest(header, directive);
            Assert.True(Utility.CompareJson(request, "ProgressiveResponseRequest.json"));
        }

        [Fact]
        public async Task ProgressiveResponseCallsDirectiveApi()
        {
            var passed = false;

            var handlerAction = new Func<HttpRequestMessage, HttpResponseMessage>(req =>
            {
                passed = req.RequestUri.AbsolutePath == "/v1/directives";
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });

            var handler = new ActionMessageHandler(handlerAction);
            var client = new HttpClient(handler) {BaseAddress = new Uri("https://localhost")};

            var response = new ProgressiveResponse("xxx", "authToken", "http://localhost", client);
            await response.SendSpeech("hello");

            Assert.True(passed);
        }
    }

    public class ActionMessageHandler:HttpMessageHandler
    {
        private Func<HttpRequestMessage,HttpResponseMessage> Action { get; }

        public ActionMessageHandler(Func<HttpRequestMessage,HttpResponseMessage> action)
        {
            Action = action;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Action(request));
        }
    }
}
