using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Alexa.NET.Tests
{
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