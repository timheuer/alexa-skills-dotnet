using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Directive.ConnectionTasks;
using Xunit;

namespace Alexa.NET.Tests
{
    public class SkillConnectionTests
    {
        [Fact]
        public void StartConnectionDirectiveSerializesCorrectly()
        {
            var task = new PrintPdfV1
            {
                Title = "title",
                Description = "description",
                Url = new Uri("http://www.example.com/flywheel.pdf")
            };
            Utility.CompareJson(task.ToConnectionDirective("none"), "PrintPDFConnection.json");
        }
    }
}
