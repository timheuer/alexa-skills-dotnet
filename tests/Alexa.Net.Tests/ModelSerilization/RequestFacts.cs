using System.IO;

using Alexa.NET.Request;
using Alexa.NET.Request.Type;

using Newtonsoft.Json;

using Xunit;

namespace Alexa.Net.Tests.ModelSerilization
{
    public class RequestTests
    {
        private const string ExamplesPath = @"ModelSerilization\Examples\";

        //[Fact]
        //public void Can_read_IntentRequest_example()
        //{
        //    const string example = "IntentRequest.json";
        //    var json = File.ReadAllText(Path.Combine(ExamplesPath, example));
        //    var convertedObj = JsonConvert.DeserializeObject<SkillRequest>(json);

        //    Assert.NotNull(convertedObj);
        //}

        //[Fact]
        //public void Can_read_LaunchRequest_example()
        //{
        //    const string example = "LaunchRequest.json";
        //    var json = File.ReadAllText(Path.Combine(ExamplesPath, example));
        //    var convertedObj = JsonConvert.DeserializeObject<SkillRequest>(json);

        //    Assert.NotNull(convertedObj);
        //    Assert.Equal(typeof(ILaunchRequest), convertedObj.Request.GetRequestType());
        //    Assert.Equal(typeof(ILaunchRequest), convertedObj.GetRequestType());
        //}

        //[Fact]
        //public void Can_read_SessionEndedRequest_example()
        //{
        //    const string example = "SessionEndedRequest.json";
        //    var json = File.ReadAllText(Path.Combine(ExamplesPath, example));
        //    var convertedObj = JsonConvert.DeserializeObject<SkillRequest>(json);

        //    Assert.NotNull(convertedObj);
        //    Assert.Equal(typeof(ISessionEndedRequest), convertedObj.Request.GetRequestType());
        //    Assert.Equal(typeof(ISessionEndedRequest), convertedObj.GetRequestType());
        //}

        //[Fact]
        //public void Can_read_slot_example()
        //{
        //    const string example = "GetUtterance.json";
        //    var json = File.ReadAllText(Path.Combine(ExamplesPath, example));
        //    var convertedObj = JsonConvert.DeserializeObject<SkillRequest>(json);

        //    var request = Assert.IsAssignableFrom<IIntentRequest>(convertedObj.Request);
        //    var slot = request.Intent.Slots["Utterance"];
        //    Assert.Equal("how are you", slot.Value);
        //}

        //[Fact]
        //public void Can_accept_new_versions()
        //{
        //    const string example = "SessionEndedRequest.json";
        //    var json = File.ReadAllText(Path.Combine(ExamplesPath, example));
        //    var convertedObj = JsonConvert.DeserializeObject<SkillRequest>(json);

        //    Assert.NotNull(convertedObj);
        //    Assert.Equal(typeof(ISessionEndedRequest), convertedObj.Request.GetRequestType());
        //    Assert.Equal(typeof(ISessionEndedRequest), convertedObj.GetRequestType());
        //}
    }
}