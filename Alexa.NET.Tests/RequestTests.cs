﻿using System.IO;
using System.Linq;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public void Can_read_LaunchRequestWithEpochTimestamp_example()
        {
			var convertedObj = GetObjectFromExample<SkillRequest>("LaunchRequestWithEpochTimestamp.json");

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
        public void Can_read_resolution()
        {
            var actual = new Resolution
            {
                Authorities = new[]{
                    new ResolutionAuthority{
                        Name="amzn1.er-authority.echo-sdk.<skill_id>.MEDIA_TYPE",
                        Status=new ResolutionStatus{Code=ResolutionStatusCode.SuccessfulMatch},
                        Values=new []{
                            new ResolutionValueContainer{Value=
                                new ResolutionValue{Name="song",Id="SONG"}
                            }
                        }
                    }
                }
            };

            Assert.True(CompareJson(actual, "Resolution.json"));
        }

        [Fact]
        public void Can_read_intent_with_entity_resolution()
        {
            var intentRequest = GetObjectFromExample<IntentRequest>("IntentWithResolution.json");
            var mediaTypeSlot = intentRequest.Intent.Slots["MediaType"];
            var mediaTitleSlot = intentRequest.Intent.Slots["MediaTitle"];

            var mediaTypeResolutionAuthority = new Resolution
            {
                Authorities = new[]{
                    new ResolutionAuthority{
                        Name="amzn1.er-authority.echo-sdk.<skill_id>.MEDIA_TYPE",
                        Status = new ResolutionStatus{Code=ResolutionStatusCode.SuccessfulMatch},
                        Values= new[]{
                            new ResolutionValueContainer{Value = new ResolutionValue{
                                    Name="song",
                                    Id="SONG"
                                }
                            }
                    }
                }
                }
            };

			var mediaTitleResolutionAuthority = new Resolution
			{
				Authorities = new[]{
					new ResolutionAuthority{
						Name="amzn1.er-authority.echo-sdk.<skill_id>.MEDIA_TITLE",
						Status = new ResolutionStatus{Code=ResolutionStatusCode.SuccessfulMatch},
						Values= new[]{
							new ResolutionValueContainer{Value = new ResolutionValue{
									Name="Rolling in the Deep",
									Id="song_id_456"
								}
							}
					}
				}
				}
			};



            Assert.True(CompareObjectJson(mediaTypeSlot.Resolution, mediaTypeResolutionAuthority));
            Assert.True(CompareObjectJson(mediaTitleSlot.Resolution, mediaTitleResolutionAuthority));
        }

        [Fact]
        public void Can_Read_SkillEventAccountLink()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("SkillEventAccountLink.json");
            var request = Assert.IsAssignableFrom<AccountLinkSkillEventRequest>(convertedObj.Request);
            Assert.Equal(request.Body.AccessToken,"testToken");
        }

        [Fact]
        public void Can_Read_SkillEventPermissionChange()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("SkillEventPermissionChange.json");
            var request = Assert.IsAssignableFrom<PermissionSkillEventRequest>(convertedObj.Request);
            Assert.Equal(request.Body.AcceptedPermissions.First().Scope, "testScope");
        }

        [Fact]
        public void Can_Read_NonSpecialisedSkillEvent()
        {
            var convertedObj = GetObjectFromExample<SkillRequest>("SkillEventEnabled.json");
            var request = Assert.IsAssignableFrom<SkillEventRequest>(convertedObj.Request);
        }

        private bool CompareObjectJson(object actual, object expected)
        {

            var actualJObject = JObject.FromObject(actual);
            var expectedJObject = JObject.FromObject(expected);

            return JToken.DeepEquals(expectedJObject, actualJObject);
        }

        private bool CompareJson(object actual, string expectedFile)
        {

            var actualJObject = JObject.FromObject(actual);
            var expected = File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
            var expectedJObject = JObject.Parse(expected);

            return JToken.DeepEquals(expectedJObject, actualJObject);
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