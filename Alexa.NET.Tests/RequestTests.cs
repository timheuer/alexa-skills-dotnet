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
        private const string IntentRequestFile = "IntentRequest.json";

        [Fact]
        public void Can_read_IntentRequest_example()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>(IntentRequestFile);

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

        [Fact]
        public void DialogState_appears_in_IntentRequest()
        {
            var request = GetObjectFromExample<SkillRequest>(IntentRequestFile);

            var actual = (IntentRequest)request.Request;


            Assert.Equal(DialogState.InProgress, actual.DialogState);
        }

        [Fact]
        public void ConfirmationState_appears_in_Intent()
		{
            var request = GetObjectFromExample<SkillRequest>(IntentRequestFile);
			var intentRequest = (IntentRequest)request.Request;
            var expected = intentRequest.Intent;


			Assert.Equal(ConfirmationStatus.Denied, expected.ConfirmationStatus);
		}

		[Fact]
        public void ConfirmationState_appears_in_Slot()
		{
			var request = GetObjectFromExample<SkillRequest>(IntentRequestFile);
			var intentRequest = (IntentRequest)request.Request;
			var expected = intentRequest.Intent.Slots["Date"];


            Assert.Equal(ConfirmationStatus.Confirmed, expected.ConfirmationStatus);
		}

        private T GetObjectFromExample<T>(string filename)
        {
            var json = File.ReadAllText(Path.Combine(ExamplesPath, filename));
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}