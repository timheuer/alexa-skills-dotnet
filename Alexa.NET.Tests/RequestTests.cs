using System.IO;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Newtonsoft.Json;
using Xunit;

namespace Alexa.NET.Tests
{
    public class RequestTests
    {
        private const string ExamplesPath = "Examples";

        [Fact]
        public void Can_read_IntentRequest_example()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("IntentRequest.json");

            Assert.NotNull(convertedObj);
            Assert.Equal(typeof(IntentRequest), convertedObj.GetRequestType());
        }

        [Fact]
        public void Can_read_LaunchRequest_example()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("LaunchRequest.json");

            Assert.NotNull(convertedObj);
            Assert.Equal(typeof(LaunchRequest), convertedObj.GetRequestType());
        }

        [Fact]
        public void Can_read_SessionEndedRequest_example()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("SessionEndedRequest.json");

            Assert.NotNull(convertedObj);
            Assert.Equal(typeof(SessionEndedRequest), convertedObj.GetRequestType());
        }

        [Fact]
        public void Can_read_slot_example()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("GetUtterance.json");

            var request = Assert.IsAssignableFrom<IntentRequest>(convertedObj.Request);
            var slot = request.Intent.Slots["Utterance"];
            Assert.Equal("how are you", slot.Value);
        }

        [Fact]
        public void Can_accept_new_versions()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("SessionEndedRequest.json");

            Assert.NotNull(convertedObj);
            Assert.Equal(typeof(SessionEndedRequest), convertedObj.GetRequestType());
        }

        private T GetObjectFromExample<T>(string filename)
        {
            var json = File.ReadAllText(Path.Combine(ExamplesPath, filename));
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}