using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public interface IProgressiveResponseDirective
    {
        [JsonRequired]
        string Type { get; }
    }
}
