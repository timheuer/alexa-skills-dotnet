using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Response;
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

        [Fact]
        public void StartConnectionDirectiveDeserializesCorrectly()
        {
            var raw = Utility.ExampleFileContent<IDirective>("PrintPDFConnection.json");
            Assert.NotNull(raw);

            var directive = Assert.IsType<StartConnectionDirective>(raw);
            Assert.Equal("none", directive.Token);
            Assert.Equal(PrintPdfV1.AssociatedUri,directive.Uri.ToString());

            Assert.IsType<PrintPdfV1>(directive.Input);
        }
    }
}
