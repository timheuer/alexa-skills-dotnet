using System;
using System.Collections.Generic;
using System.IO;
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
            Assert.True(Utility.CompareJson(convertedObj,IntentRequestFile));
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
            var sessionEndedRequest = Assert.IsType<SessionEndedRequest>(convertedObj.Request);
            Assert.Equal(ErrorType.InvalidResponse,sessionEndedRequest.Error.Type);
            Assert.Equal("test message", sessionEndedRequest.Error.Message);
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
        public void Can_read_scopes_example()
        {
            var convertedObjContext = GetObjectFromExample<Context>("GetScopes.json");

            Assert.NotNull(convertedObjContext);
            var scope = convertedObjContext.System.User.Permissions.Scopes["alexa::devices:all:geolocation:read"];
            Assert.Equal("GRANTED", scope.Status);
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
            Assert.True(request.EventCreationTime.HasValue);
            Assert.True(request.EventPublishingTime.HasValue);
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

        [Fact]
        public void Can_Handle_New_Intent()
        {
            if (!RequestConverter.RequestConverters.Any(c => c is NewIntentRequestTypeConverter))
            {
                RequestConverter.RequestConverters.Add(new NewIntentRequestTypeConverter());
            }

            var request = GetObjectFromExample<SkillRequest>("NewIntent.json");
            Assert.IsType<NewIntentRequest>(request.Request);
            Assert.True(((NewIntentRequest) request.Request).TestProperty);
        }

        [Fact]
        public void New_Request_Timestamp_validated_By_RequestVerification()
        {
            var request = new SkillRequest
            {
                Request = new LaunchRequest { Timestamp = DateTime.Now.AddMinutes(1) }
            };
            Assert.True(RequestVerification.RequestTimestampWithinTolerance(request));
        }

        [Fact]
        public void Replay_Attack_Timestamp_Invalidated_By_RequestVerification()
        {
            var request = new SkillRequest
            {
                Request = new LaunchRequest { Timestamp = DateTime.Now.AddMinutes(3)}
            };
            Assert.False(RequestVerification.RequestTimestampWithinTolerance(request));
        }

        [Fact]
        public void GeolocationDataDeserializesCorrectly()
        {
            var locationData = Utility.ExampleFileContent<Geolocation>("Geolocation.json");
            Assert.Equal(LocationServiceAccess.Enabled,locationData.LocationServices.Access);
            Assert.Equal(LocationServiceStatus.Running, locationData.LocationServices.Status);

            var expectedDate = DateTimeOffset.Parse("2018-12-14T07:05:48Z");
            Assert.Equal(expectedDate,locationData.Timestamp);

            Assert.Equal(38.2,locationData.Coordinate.Latitude);
            Assert.Equal(28.3, locationData.Coordinate.Longitude);
            Assert.Equal(12.1, locationData.Coordinate.Accuracy);

            Assert.Equal(120.1,locationData.Altitude.Altitude);
            Assert.Equal(30.1, locationData.Altitude.Accuracy);

            Assert.Equal(180.0,locationData.Heading.Direction);
            Assert.Equal(5.0, locationData.Heading.Accuracy);

            Assert.Equal(10.0, locationData.Speed.Speed);
            Assert.Equal(1.1, locationData.Speed.Accuracy);
        }

        [Fact]
        public void Can_Read_Person_Information()
        {
            var request = GetObjectFromExample<SkillRequest>("BuiltInIntentRequest.json");
            Assert.NotNull(request.Context.System.Person);
            Assert.Equal("amzn1.ask.account.personid",request.Context.System.Person.PersonId);
            Assert.Equal("Atza|BBBBBBB", request.Context.System.Person.AccessToken);
            Assert.Equal(300,request.Context.System.Person.AuthenticationConfidenceLevel.Level);
        }

        [Fact]
        public void HandleConnectionsSendResponseRequest()
        {
            var request = GetObjectFromExample<Request.Type.Request>("ConnectionsResponseRequest.json");
            var askFor = Assert.IsType<AskForPermissionRequest>(request);
            Assert.Equal("AskFor",askFor.Name);
            Assert.Equal(PermissionStatus.Denied,askFor.Payload.Status);
            Assert.Equal("alexa::alerts:reminders:skill:readwrite",askFor.Payload.PermissionScope);
            Assert.Equal(200,askFor.Status.Code);
            Assert.Equal("Test Message",askFor.Status.Message);
            Utility.CompareJson(askFor, "ConnectionsResponseRequest.json");
        }

        [Fact]
        public void MultiValueSlot()
        {
            var slots = Utility.ExampleFileContent<Dictionary<string, Slot>>("MultiValueSlot.json");
            Assert.Single(slots);
            Assert.True(Utility.CompareJson(slots,"MultiValueSlot.json"));
        }

        [Fact]
        public void HandleAlexaSmartPropertiesSupport()
        {
            var request = GetObjectFromExample<SkillRequest>("SmartPropertiesIntentRequest.json");
            Assert.NotNull(request.Context.System.Unit);
            Assert.Equal("amzn1.ask.unit.A1B2C3",request.Context.System.Unit.UnitID);
            Assert.Equal("amzn1.alexa.unit.did.X7Y8Z9", request.Context.System.Unit.PersistentUnitID);
            Assert.Equal("amzn1.alexa.endpoint.AABBCC010101010101010101",request.Context.System.Device.PersistentEndpointID);
        }

        private T GetObjectFromExample<T>(string filename)
        {
            var json = File.ReadAllText(Path.Combine(ExamplesPath, filename));
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}