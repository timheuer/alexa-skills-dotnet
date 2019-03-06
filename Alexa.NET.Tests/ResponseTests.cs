using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Directive.Templates;
using Alexa.NET.Response.Directive.Templates.Types;
using Alexa.NET.Response.Ssml;
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

        [Fact]
        public void RepromptStringGeneratesPlainTextOutput()
        {
            var result = new Reprompt("text");
            Assert.IsType<PlainTextOutputSpeech>(result.OutputSpeech);
            var plainText = (PlainTextOutputSpeech)result.OutputSpeech;
            Assert.Equal("text", plainText.Text);
        }

        [Fact]
        public void RepromptSsmlGeneratesPlainTextOutput()
        {
            var speech = new Speech(new PlainText("text"));
            var result = new Reprompt(speech);
            Assert.IsType<SsmlOutputSpeech>(result.OutputSpeech);
            var ssmlText = (SsmlOutputSpeech)result.OutputSpeech;
            Assert.Equal(speech.ToXml(), ssmlText.Ssml);
        }

        [Fact]
        public void DeserializesEmptyResponse()
        {
            var response = ResponseBuilder.Empty();
            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.Equal("1.0", response.Version);
        }

        [Fact]
        public void DeserializesSimpleCardResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Card = new SimpleCard
            {
                Title = "Card Title",
                Content = "Card Content"
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Card is SimpleCard);

            var card1 = (SimpleCard)response.Response.Card;
            var card2 = (SimpleCard)deserialized.Response.Card;

            Assert.Equal(card1.Title, card2.Title);
            Assert.Equal(card1.Content, card2.Content);
        }

        [Fact]
        public void DeserializesStandardCardResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Card = new StandardCard
            {
                Title = "Card Title",
                Content = "Card Content",
                Image = new CardImage
                {
                    LargeImageUrl = "https://foo.com/large-image.png",
                    SmallImageUrl = "https://foo.com/small-image.png",
                },
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Card is StandardCard);

            var card1 = (StandardCard)response.Response.Card;
            var card2 = (StandardCard)deserialized.Response.Card;

            Assert.Equal(card1.Title, card2.Title);
            Assert.Equal(card1.Content, card2.Content);
            Assert.Equal(card1.Image.SmallImageUrl, card2.Image.SmallImageUrl);
            Assert.Equal(card1.Image.LargeImageUrl, card2.Image.LargeImageUrl);
        }

        [Fact]
        public void DeserializesLinkAccountCardResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Card = new LinkAccountCard();

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Card is LinkAccountCard);

            var card1 = (LinkAccountCard)response.Response.Card;
            var card2 = (LinkAccountCard)deserialized.Response.Card;
        }

        [Fact]
        public void DeserializesAskForPermissionsConsentCardResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Card = new AskForPermissionsConsentCard
            {
                Permissions = new List<string>
                {
                    "Permission1",
                    "Permission2"
                }
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Card is AskForPermissionsConsentCard);

            var card1 = (AskForPermissionsConsentCard)response.Response.Card;
            var card2 = (AskForPermissionsConsentCard)deserialized.Response.Card;

            Assert.Equal(card1.Permissions.First(), card2.Permissions.First());
            Assert.Equal(card1.Permissions.Last(), card2.Permissions.Last());
        }

        [Fact]
        public void DeserializesPlainTextOutputSpeechResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = "Plain Text Output Speech"
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.OutputSpeech is PlainTextOutputSpeech);

            var speech1 = (PlainTextOutputSpeech)response.Response.OutputSpeech;
            var speech2 = (PlainTextOutputSpeech)deserialized.Response.OutputSpeech;

            Assert.Equal(speech1.Text, speech2.Text);
        }

        [Fact]
        public void DeserializesSsmlOutputSpeechResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.OutputSpeech = new SsmlOutputSpeech
            {
                Ssml = "SSML Output Speech"
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.OutputSpeech is SsmlOutputSpeech);

            var speech1 = (SsmlOutputSpeech)response.Response.OutputSpeech;
            var speech2 = (SsmlOutputSpeech)deserialized.Response.OutputSpeech;

            Assert.Equal(speech1.Ssml, speech2.Ssml);
        }

        [Fact]
        public void DeserializesAudioPlayerPlayDirectiveResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new AudioPlayerPlayDirective
                {
                    AudioItem = new AudioItem
                    {
                        Metadata = new AudioItemMetadata
                        {
                            Art = new AudioItemSources
                            {
                                Sources = new List<AudioItemSource>
                                {
                                    new AudioItemSource
                                    {
                                        Url = "https://foo.com/art-source"
                                    }
                                }
                            },
                            BackgroundImage = new AudioItemSources
                            {
                                Sources = new List<AudioItemSource>
                                {
                                    new AudioItemSource
                                    {
                                        Url = "https://foo.com/background-image-source"
                                    }
                                }
                            },
                            Subtitle = "subtitle",
                            Title = "title",
                        },
                        Stream = new AudioItemStream
                        {
                            ExpectedPreviousToken = "expected-previous-token",
                            OffsetInMilliseconds = 100,
                            Token = "token",
                            Url = "https://foo.com/audio-item-stream"
                        }
                    },
                    PlayBehavior = PlayBehavior.Enqueue
                }
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is AudioPlayerPlayDirective);

            var directive1 = (AudioPlayerPlayDirective)response.Response.Directives.First();
            var directive2 = (AudioPlayerPlayDirective)deserialized.Response.Directives.First();

            Assert.Equal(directive1.PlayBehavior, directive2.PlayBehavior);
            Assert.Equal(directive1.AudioItem.Stream.Url, directive2.AudioItem.Stream.Url);
            Assert.Equal(directive1.AudioItem.Stream.Token, directive2.AudioItem.Stream.Token);
            Assert.Equal(directive1.AudioItem.Stream.OffsetInMilliseconds, directive2.AudioItem.Stream.OffsetInMilliseconds);
            Assert.Equal(directive1.AudioItem.Stream.ExpectedPreviousToken, directive2.AudioItem.Stream.ExpectedPreviousToken);
            Assert.Equal(directive1.AudioItem.Metadata.Title, directive2.AudioItem.Metadata.Title);
            Assert.Equal(directive1.AudioItem.Metadata.Subtitle, directive2.AudioItem.Metadata.Subtitle);
            Assert.Equal(directive1.AudioItem.Metadata.Art.Sources.First().Url, directive2.AudioItem.Metadata.Art.Sources.First().Url);
            Assert.Equal(directive1.AudioItem.Metadata.BackgroundImage.Sources.First().Url, directive2.AudioItem.Metadata.BackgroundImage.Sources.First().Url);
        }

        [Fact]
        public void DeserializesClearQueueDirectiveResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new ClearQueueDirective
                {
                    ClearBehavior  = ClearBehavior.ClearAll,
                }
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is ClearQueueDirective);

            var directive1 = (ClearQueueDirective)response.Response.Directives.First();
            var directive2 = (ClearQueueDirective)deserialized.Response.Directives.First();

            Assert.Equal(directive1.ClearBehavior, directive2.ClearBehavior);
        }

        [Fact]
        public void DeserializesDialogConfirmIntentResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new DialogConfirmIntent
                {
                    UpdatedIntent = new Intent
                    {
                        ConfirmationStatus = "confirmation status",
                        Name = "name",
                        Slots = new Dictionary<string, Slot>
                        {
                            {
                                "slot-key", new Slot
                                {
                                    Name = "slot-name",
                                    Value = "slot-value",
                                    ConfirmationStatus = "confirmation status",
                                    Resolution = new Resolution
                                    {
                                        Authorities = new ResolutionAuthority[]
                                        {
                                            new ResolutionAuthority
                                            {
                                                Name = "name",
                                                Status  = new ResolutionStatus
                                                {
                                                    Code = "code"
                                                },
                                                Values = new ResolutionValueContainer[]
                                                {
                                                    new ResolutionValueContainer
                                                    {
                                                        Value = new ResolutionValue
                                                        {
                                                            Name = "name",
                                                            Id = "id"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is DialogConfirmIntent);

            var directive1 = (DialogConfirmIntent)response.Response.Directives.First();
            var directive2 = (DialogConfirmIntent)deserialized.Response.Directives.First();

            Assert.Equal(directive1.UpdatedIntent.Name, directive2.UpdatedIntent.Name);
            Assert.Equal(directive1.UpdatedIntent.ConfirmationStatus, directive2.UpdatedIntent.ConfirmationStatus);

            var slot1 = directive1.UpdatedIntent.Slots.First();
            var slot2 = directive2.UpdatedIntent.Slots.First();

            Assert.Equal(slot1.Key, slot2.Key);
            Assert.Equal(slot1.Value.Name, slot2.Value.Name);
            Assert.Equal(slot1.Value.ConfirmationStatus, slot2.Value.ConfirmationStatus);
            Assert.Equal(slot1.Value.Value, slot2.Value.Value);

            var authority1 = slot1.Value.Resolution.Authorities.First();
            var authority2 = slot2.Value.Resolution.Authorities.First();

            Assert.Equal(authority1.Name, authority2.Name);
            Assert.Equal(authority1.Status.Code, authority2.Status.Code);
            Assert.Equal(authority1.Values.First().Value.Name, authority2.Values.First().Value.Name);
            Assert.Equal(authority1.Values.First().Value.Id, authority2.Values.First().Value.Id);
        }

        [Fact]
        public void DeserializesDialogConfirmSlotResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new DialogConfirmSlot("slot name"),
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is DialogConfirmSlot);

            var directive1 = (DialogConfirmSlot)response.Response.Directives.First();
            var directive2 = (DialogConfirmSlot)deserialized.Response.Directives.First();

            Assert.Equal(directive1.SlotName, directive2.SlotName);
        }

        [Fact]
        public void DeserializesDialogDelegateResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new DialogDelegate()
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is DialogDelegate);

            var directive1 = (DialogDelegate)response.Response.Directives.First();
            var directive2 = (DialogDelegate)deserialized.Response.Directives.First();
        }

        [Fact]
        public void DeserializesDialogElicitSlotResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new DialogElicitSlot("slot name")
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is DialogElicitSlot);

            var directive1 = (DialogElicitSlot)response.Response.Directives.First();
            var directive2 = (DialogElicitSlot)deserialized.Response.Directives.First();

            Assert.Equal(directive1.SlotName, directive2.SlotName);
        }

        [Fact]
        public void DeserializesDisplayRenderTemplateDirectiveResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new DisplayRenderTemplateDirective
                {
                    Template = new BodyTemplate1
                    {
                        BackButton = "back button",
                        BackgroundImage = new TemplateImage
                        {
                            ContentDescription = "content description",
                            Sources = new List<ImageSource>
                            {
                                new ImageSource
                                {
                                    Height = 100,
                                    Size = "size",
                                    Url = "url",
                                    Width = 100
                                }
                            }
                        },
                    }
                }
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is DisplayRenderTemplateDirective);

            var directive1 = (DisplayRenderTemplateDirective)response.Response.Directives.First();
            var directive2 = (DisplayRenderTemplateDirective)deserialized.Response.Directives.First();
        }

        [Fact]
        public void DeserializesHintDirectiveResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new HintDirective
                {
                    Hint = new Hint
                    {
                        Text = "hint text",
                        Type = "hint type"
                    }
                }
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is HintDirective);

            var directive1 = (HintDirective)response.Response.Directives.First();
            var directive2 = (HintDirective)deserialized.Response.Directives.First();

            Assert.Equal(directive1.Hint.Text, directive2.Hint.Text);
            Assert.Equal(directive1.Hint.Type, directive2.Hint.Type);
        }

        [Fact]
        public void DeserializesStopDirectiveResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new StopDirective(),
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is StopDirective);

            var directive1 = (StopDirective)response.Response.Directives.First();
            var directive2 = (StopDirective)deserialized.Response.Directives.First();
        }

        [Fact]
        public void DeserializesVideoAppDirectiveResponse()
        {
            var response = ResponseBuilder.Empty();

            response.Response.Directives = new List<IDirective>
            {
                new VideoAppDirective
                {
                    VideoItem = new VideoItem("source")
                    {
                        Metadata = new VideoItemMetadata
                        {
                            Title = "title",
                            Subtitle = "subtitle"
                        }
                    }
                },
            };

            var serialized = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<SkillResponse>(serialized);

            Assert.True(deserialized.Response.Directives.First() is VideoAppDirective);

            var directive1 = (VideoAppDirective)response.Response.Directives.First();
            var directive2 = (VideoAppDirective)deserialized.Response.Directives.First();

            Assert.Equal(directive1.VideoItem.Source, directive2.VideoItem.Source);
            Assert.Equal(directive1.VideoItem.Metadata.Title, directive2.VideoItem.Metadata.Title);
            Assert.Equal(directive1.VideoItem.Metadata.Subtitle, directive2.VideoItem.Metadata.Subtitle);
        }

        private bool CompareJson(object actual, JObject expected)
        {
            var actualJObject = JObject.FromObject(actual);
            return JToken.DeepEquals(expected, actualJObject);
        }
    }
}