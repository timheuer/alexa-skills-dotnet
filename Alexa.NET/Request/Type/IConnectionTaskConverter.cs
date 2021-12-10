using Alexa.NET.ConnectionTasks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.NET.Request.Type
{
    public interface IConnectionTaskConverter
    {
        bool CanConvert(JObject jObject);
        IConnectionTask Convert(JObject jObject);
    }
}
