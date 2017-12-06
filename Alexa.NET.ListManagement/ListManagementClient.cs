using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;

namespace Alexa.NET
{
    public class ListManagementClient
    {
        public const string ListManagementDomain = "https://api.amazonalexa.com";
        public HttpClient Client { get; }
        public string AccessToken { get; }

        public ListManagementClient(string token) : this(token,new HttpClient{BaseAddress = new Uri(ListManagementDomain,UriKind.Absolute)})
        {
        }

        public ListManagementClient(string token, HttpClient client)
        {
            AccessToken = token;
            Client = client;
        }


    }
}
