using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Directive.Templates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var actual = new VideoAppDirective { VideoItem = videoItem };

            Assert.True(Utility.CompareJson(actual, "VideoAppDirectiveWithMetadata.json"));
        }

        [Fact]
        public void Create_VideoAppDirective_FromSource()
        {
            var actual = new VideoAppDirective("https://www.example.com/video/sample-video-1.mp4");
            Assert.True(Utility.CompareJson(actual, "VideoAppDirective.json"));
        }

        [Fact]
        public void Create_HintDirective()
        {
            var actual = new HintDirective { Hint = new Hint { Text = "sample text", Type = TextType.Plain } };
            var expected = JObject.Parse(@"{
            ""type"": ""Hint"",
            ""hint"": {
                ""type"": ""PlainText"",
                ""text"": ""sample text""
            }
        }");
            Assert.True(CompareJson(actual, expected));
        }

        [Fact]
        public void AudioPlayerGeneratesCorrectJson()
        {
            var directive = new AudioPlayerPlayDirective
            {
                PlayBehavior = PlayBehavior.Enqueue,
                AudioItem = new AudioItem
                {
                    Stream = new AudioItemStream
                    {
                        Url = "https://url-of-the-stream-to-play",
                        Token = "opaque token representing this stream",
                        ExpectedPreviousToken = "opaque token representing the previous stream"
                    }
                }
            };
            Assert.True(Utility.CompareJson(directive, "AudioPlayerWithoutMetadata.json"));
        }

        [Fact]
        public void AudioPlayerWithMetadataGeneratesCorrectJson()
        {
            var directive = new AudioPlayerPlayDirective
            {
                PlayBehavior = PlayBehavior.Enqueue,
                AudioItem = new AudioItem
                {
                    Stream = new AudioItemStream
                    {
                        Url = "https://url-of-the-stream-to-play",
                        Token = "opaque token representing this stream",
                        ExpectedPreviousToken = "opaque token representing the previous stream"
                    },
                    Metadata = new AudioItemMetadata
                    {
                        Title = "title of the track to display",
                        Subtitle = "subtitle of the track to display",
                        Art = new AudioItemSources
                        {
                            Sources = new[] { new AudioItemSource("https://url-of-the-album-art-image.png") }.ToList()
                        },
                        BackgroundImage = new AudioItemSources { Sources = new[] { new AudioItemSource("https://url-of-the-background-image.png") }.ToList() }
                    }
                }
            };
            Assert.True(Utility.CompareJson(directive, "AudioPlayerWithMetadata.json"));
        }

        [Fact]
        public void AudioPlayerWithMetadataDeserializesCorrectly()
        {
            var audioPlayer = Utility.ExampleFileContent<AudioPlayerPlayDirective>("AudioPlayerWithMetadata.json");
            Assert.Equal("title of the track to display", audioPlayer.AudioItem.Metadata.Title);
            Assert.Equal("subtitle of the track to display", audioPlayer.AudioItem.Metadata.Subtitle);
            Assert.Single(audioPlayer.AudioItem.Metadata.Art.Sources);
            Assert.Single(audioPlayer.AudioItem.Metadata.BackgroundImage.Sources);
            Assert.Equal("https://url-of-the-album-art-image.png", audioPlayer.AudioItem.Metadata.Art.Sources.First().Url);
            Assert.Equal("https://url-of-the-background-image.png", audioPlayer.AudioItem.Metadata.BackgroundImage.Sources.First().Url);
        }

        [Fact]
        public void AudioPlayerIgnoresMetadataWhenNull()
        {
            var audioPlayer = Utility.ExampleFileContent<AudioPlayerPlayDirective>("AudioPlayerWithoutMetadata.json");
            Assert.Null(audioPlayer.AudioItem.Metadata);
            Assert.Equal("https://url-of-the-stream-to-play", audioPlayer.AudioItem.Stream.Url);
        }

        private bool CompareJson(object actual, JObject expected)
        {
            var actualJObject = JObject.FromObject(actual);
            return JToken.DeepEquals(expected, actualJObject);
        }
    }
}