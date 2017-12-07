using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Alexa.NET.Tests
{
    public class ActionMessageHandler:HttpMessageHandler
    {
        private Func<HttpRequestMessage,Task<HttpResponseMessage>> Action { get; }

        public ActionMessageHandler(Func<HttpRequestMessage,HttpResponseMessage> action)
        {
            Action = r => Task.FromResult(action(r));
        }

        public ActionMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> action)
        {
            Action = action;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Action(request);
        }
    }
}