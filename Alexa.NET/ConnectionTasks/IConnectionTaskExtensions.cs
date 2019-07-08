using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response.Directive;

namespace Alexa.NET.ConnectionTasks
{
    public static class IConnectionTaskExtensions
    {

        public static StartConnectionDirective ToConnectionDirective(this IConnectionTask task, string token = null)
        {
            return new StartConnectionDirective(task, token);
        }
    }
}
