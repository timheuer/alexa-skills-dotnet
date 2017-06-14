using System.IO;
using System.Linq;
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
        public void IntentRequest_Generates_Correct_Name_and_Signature()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("IntentRequest.json");
            var intent = ((IntentRequest)convertedObj.Request).Intent;
            Assert.Equal("GetZodiacHoroscopeIntent", intent.Name);
            Assert.Equal("GetZodiacHoroscopeIntent", intent.Signature);
            Assert.Equal("GetZodiacHoroscopeIntent", intent.Signature.Action);
        }

        [Fact]
        public void BuiltInRequest_Generates_Correct_Signature()
        {
            //Multiple asserts as the IntentSignature state is a single output that should be treated as an immutable object - either all right or wrong.
			//AMAZON.AddAction<object@Book,targetCollection@ReadingList>
			var convertedObj = GetObjectFromExample<SkillRequest>("BuiltInIntentRequest.json");
			var signature = ((IntentRequest)convertedObj.Request).Intent.Signature;
			Assert.Equal("AddAction", signature.Action);
            Assert.Equal("AMAZON", signature.Namespace);
            Assert.Equal(2, signature.Properties.Count);

            var first = signature.Properties.First();
            var second = signature.Properties.Skip(1).First();

            Assert.Equal("object", first.Key);
            Assert.Equal("Book", first.Value.Entity);
            Assert.True(string.IsNullOrWhiteSpace(first.Value.Property));

            Assert.Equal("targetCollection", second.Key);
            Assert.Equal("ReadingList", second.Value.Entity);
            Assert.True(string.IsNullOrWhiteSpace(first.Value.Property));
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