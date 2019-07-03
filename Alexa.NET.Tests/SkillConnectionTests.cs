using System;
using Alexa.NET.ConnectionTasks;
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
                    Status = new SessionResumedRequestCauseStatus(200,"OK")
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
    }
}
