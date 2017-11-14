using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response
{
    public class ProgressiveResponse
    {
        private HttpClient Client { get; }
        private ProgressiveResponseHeader Header { get; }

        public static bool IsSupported(SkillRequest request)
        {
            return !string.IsNullOrWhiteSpace(request?.Request?.RequestId) &&
                   !string.IsNullOrWhiteSpace(request.Context?.System?.ApiAccessToken) &&
                   !string.IsNullOrWhiteSpace(request.Context?.System?.ApiEndpoint);
        }


        public ProgressiveResponse(SkillRequest request) : this(request, new HttpClient())
        {
            
        }

        public ProgressiveResponse(SkillRequest request,HttpClient client) : this(
            request.Request.RequestId,
            request.Context.System.ApiAccessToken,
            request.Context.System.ApiEndpoint,client)
        {
        }

        public ProgressiveResponse(string requestId, string authToken, string baseAddress):this(requestId,authToken,baseAddress,new HttpClient())
        {

        }

        public ProgressiveResponse(string requestId, string authToken,string baseAddress, HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",authToken);
            Header = new ProgressiveResponseHeader(requestId);
        }

        public ProgressiveResponse(HttpClient client)
        {
            Client = client;
        }

        public Task<HttpResponseMessage> SendSpeech(Ssml.Speech speech)
        {
            return SendSpeech(speech.ToXml());
        }

        public Task<HttpResponseMessage> SendSpeech(string speech)
        {
            return Send(new VoicePlayerSpeakDirective(speech));
        }

        public Task<HttpResponseMessage> Send(IProgressiveResponseDirective directive)
        {
            var request = new ProgressiveResponseRequest
            {
                Header = Header,
                Directive = directive
            };
            var json = JObject.FromObject(request).ToString();
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            return Client.PostAsync(new Uri("/v1/directives", UriKind.Relative), httpContent);
        }
    }
}
