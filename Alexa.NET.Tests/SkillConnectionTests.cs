using System;
using Alexa.NET.ConnectionTasks;
using Alexa.NET.ConnectionTasks.Inputs;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
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
                Url = "http://www.example.com/flywheel.pdf",
                Context = new ConnectionTaskContext { ProviderId="your-provider-skill-id"}
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
            Assert.Equal(PrintPdfV1.AssociatedUri,directive.Uri);

            Assert.IsType<PrintPdfV1>(directive.Input);
        }

        [Fact]
        public void SessionResumedSerializesProperly()
        {
            var task = new SessionResumedRequest
            {
                RequestId = "string",
                Timestamp = new DateTime(2019,07,03),
                Locale = "en-GB",
                OriginIpAddress = "string",
                Cause = new SessionResumedRequestCause
                {
                    Type="ConnectionCompleted",
                    Token="1234",
                    Status = new TaskStatus(200,"OK")
                }
            };
            Utility.CompareJson(task, "SessionResumedRequest.json");
        }

        [Fact]
        public void SessionResumedDeserializesProperly()
        {
            var result = Utility.ExampleFileContent<Request.Type.Request>("SessionResumedRequest.json");
            var request = Assert.IsType<SessionResumedRequest>(result);

            Assert.Equal("1234",request.Cause.Token);
            Assert.Equal(200,request.Cause.Status.Code);
            Assert.Equal("OK",request.Cause.Status.Message);
        }

        [Fact]
        public void LaunchRequestWithTaskDeserializesCorrectly()
        {
            var result = Utility.ExampleFileContent<LaunchRequest>("LaunchRequestWithTask.json");
            Assert.NotNull(result.Task);
            Assert.Equal("AMAZON.PrintPDF",result.Task.Name);
            Assert.Equal("1",result.Task.Version);
            Assert.IsType<PrintPdfV1>(result.Task.Input);
        }

        [Fact]
        public void  TestCompleteTaskDirective()
        {
            var directive = new CompleteTaskDirective(200, "return as desired");
            Assert.True(Utility.CompareJson(directive,"CompleteTaskDirective.json"));
        }

        [Fact]
        public void PrintImageConnectionComparison()
        {
            var directive = new PrintImageV1
            {
                Title = "Flywheel Document",
                Description = "Flywheel",
                ImageV1Type = PrintImageV1Type.JPEG,
                Url = "http://www.example.com/flywheel.jpeg"
            }.ToConnectionDirective();
            Assert.Equal(PrintImageV1.AssociatedUri,directive.Uri);
            Assert.True(Utility.CompareJson(directive,"PrintImageConnection.json"));
            Assert.IsType<PrintImageV1>(Utility.ExampleFileContent<StartConnectionDirective>("PrintImageConnection.json").Input);
        }

        [Fact]
        public void PrintPDFConnectionComparison()
        {
            var directive = new PrintPdfV1
            {
                Title = "title",
                Description = "description",
                Url = "http://www.example.com/flywheel.pdf",
                Context = new ConnectionTaskContext { ProviderId = "your-provider-skill-id" }
            }.ToConnectionDirective("none");
            Assert.Equal(PrintPdfV1.AssociatedUri, directive.Uri);
            Assert.True(Utility.CompareJson(directive, "PrintPDFConnection.json"));
            Assert.IsType<PrintPdfV1>(Utility.ExampleFileContent<StartConnectionDirective>("PrintPDFConnection.json").Input);
        }

        [Fact]
        public void PrintWebPageConnectionComparison()
        {
            var directive = new PrintWebPageV1
            {
                Title = "title",
                Description = "description",
                Url = "http://www.example.com/flywheel.html"
            }.ToConnectionDirective();
            Assert.Equal(PrintWebPageV1.AssociatedUri, directive.Uri);
            Assert.True(Utility.CompareJson(directive, "PrintWebPageConnection.json"));
            Assert.IsType<PrintPdfV1>(Utility.ExampleFileContent<StartConnectionDirective>("PrintPDFConnection.json").Input);
        }

    }
}
