﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response
{
    public class ProgressiveResponse
    {
        public HttpClient Client { get; set; }
        public ProgressiveResponseHeader Header { get; set; }

        public static bool IsSupported(SkillRequest request)
        {
            return !string.IsNullOrWhiteSpace(request?.Request?.RequestId) &&
                   !string.IsNullOrWhiteSpace(request.Context?.System?.ApiAccessToken) &&
                   !string.IsNullOrWhiteSpace(request.Context?.System?.ApiEndpoint);
        }


        public ProgressiveResponse(SkillRequest request) : this(request, new())
        {
            
        }

        public ProgressiveResponse(SkillRequest request,HttpClient client) : this(
            request?.Request?.RequestId,
            request?.Context?.System?.ApiAccessToken,
            request?.Context?.System?.ApiEndpoint,client)
        {
        }

        public ProgressiveResponse(string requestId, string authToken, string baseAddress):this(requestId,authToken,baseAddress,new())
        {

        }

        public ProgressiveResponse(string requestId, string authToken,string baseAddress, HttpClient client)
        {
            Client = client;
            if (!string.IsNullOrWhiteSpace(baseAddress))
            {
                Client.BaseAddress = new(baseAddress, UriKind.Absolute);
            }

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                Client.DefaultRequestHeaders.Authorization = new("Bearer", authToken);
            }

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                Header = new(requestId);
            }
        }

        public ProgressiveResponse(ProgressiveResponseHeader header,HttpClient client)
        {
            Client = client;
            Header = header;
        }

        public ProgressiveResponse()
        {

        }

        public Task<HttpResponseMessage> SendSpeech(Ssml.Speech ssml)
        {
            return Send(new VoicePlayerSpeakDirective(ssml));
        }

        public Task<HttpResponseMessage> SendSpeech(string ssml)
        {
            return Send(new VoicePlayerSpeakDirective(ssml));
        }

        public bool CanSend()
        {
            return Header != null && Client != null;
        }

        public Task<HttpResponseMessage> Send(IProgressiveResponseDirective directive)
        {
            if (directive == null || !CanSend())
            {
                return Task.FromResult((HttpResponseMessage)null);
            }

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
