using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Alexa.NET.Tests
{
    public class ResponseTests
    {
        private const string ExamplesPath = @"Examples";

        [Fact]
        public void Should_create_same_json_response_as_example()
        {
            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                SessionAttributes = new Dictionary<string, object>
                {
                    {
                        "supportedHoriscopePeriods", new
                        {
                            daily = true,
                            weekly = false,
                            monthly = false
                        }
                    }
                },
                Response = new ResponseBody
                {
                    OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text =
                            "Today will provide you a new learning opportunity. Stick with it and the possibilities will be endless. Can I help you with anything else?"
                    },
                    Card = new SimpleCard
                    {
                        Title = "Horoscope",
                        Content =
                            "Today will provide you a new learning opportunity. Stick with it and the possibilities will be endless."
                    },
                    ShouldEndSession = false
                }
            };

            var json = JsonConvert.SerializeObject(skillResponse, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            const string example = "Response.json";
            var workingJson = File.ReadAllText(Path.Combine(ExamplesPath, example));

            workingJson = Regex.Replace(workingJson, @"\s", "");
            json = Regex.Replace(json, @"\s", "");

            Assert.Equal(workingJson, json);
        }

        [Fact]
        public void Creates_VideoAppDirective()
        {
            var videoItem = new VideoItem("https://www.example.com/video/sample-video-1.mp4")
            {
                Metadata = new VideoItemMetadata
                {
                    Title = "Title for Sample Video",
                    Subtitle = "Secondary Title for Sample Video"
                }
            };
            var actual = new VideoAppDirective{VideoItem=videoItem};

            Assert.True(Utility.CompareJson(actual, "VideoAppDirectiveWithMetadata.json"));
        }

        [Fact]
        public void Create_VideoAppDirective_FromSource()
        {
            var actual = new VideoAppDirective("https://www.example.com/video/sample-video-1.mp4");
            Assert.True(Utility.CompareJson(actual, "VideoAppDirective.json"));
        }
    }
}