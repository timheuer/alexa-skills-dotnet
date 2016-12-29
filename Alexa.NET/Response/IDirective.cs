using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.NET.Response
{
    public interface IDirective
    {
        [JsonRequired]
        string Type { get; }
    }
}
