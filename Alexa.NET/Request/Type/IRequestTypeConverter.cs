using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.NET.Request.Type
{
    public interface IRequestTypeConverter
    {
        bool CanConvert(string requestType);
        Request Convert(string requestType);
    }
}
