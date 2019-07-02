using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.NET.Response.Directive.ConnectionTasks
{
    public static class IConnectionTaskExtensions
    {

        public static StartConnectionDirective ToConnectionDirective(this IConnectionTask task, string token)
        {
            return new StartConnectionDirective(task, token);
        }
    }
}
