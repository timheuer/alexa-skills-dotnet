using System;
using System.Linq;
using Alexa.NET.Request.SmartHome;
using Alexa.NET.Response.SmartHome;
using Newtonsoft.Json;
using Xunit;

namespace Alexa.NET.Tests
{
    public class SmartHomeTests
    {
        private const string ExamplesPath = "Examples";

        [Fact]
        public void Can_Deserialize_Discovery_Request()
        {
            var request = Utility.ExampleFileContent<SmartHomeRequest>("SmartHomeDiscoveryRequest.json");

            Assert.NotNull(request);
            Assert.NotNull(request.Directive);
            Assert.Equal("Alexa.Discovery", request.Directive.Header.Namespace);
            Assert.Equal("Discover", request.Directive.Header.Name);
            Assert.Equal("3", request.Directive.Header.PayloadVersion);
            Assert.NotNull(request.Directive.Header.MessageId);
        }

        [Fact]
        public void Can_Deserialize_Discovery_Request_Payload()
        {
            var request = Utility.ExampleFileContent<SmartHomeRequest>("SmartHomeDiscoveryRequest.json");
            var payload = JsonConvert.DeserializeObject<Request.SmartHome.DiscoveryRequestPayload>(request.Directive.Payload.ToString());

            Assert.NotNull(payload);
            Assert.NotNull(payload.Scope);
            Assert.Equal("BearerToken", payload.Scope.Type);
            Assert.Equal("access-token-from-Amazon", payload.Scope.Token);
        }

        [Fact]
        public void Can_Deserialize_PowerController_TurnOn_Request()
        {
            var request = Utility.ExampleFileContent<SmartHomeRequest>("SmartHomePowerControllerTurnOn.json");

            Assert.NotNull(request);
            Assert.NotNull(request.Directive);
            Assert.Equal("Alexa.PowerController", request.Directive.Header.Namespace);
            Assert.Equal("TurnOn", request.Directive.Header.Name);
            Assert.Equal("3", request.Directive.Header.PayloadVersion);
            Assert.NotNull(request.Directive.Header.CorrelationToken);
            
            Assert.NotNull(request.Directive.Endpoint);
            Assert.Equal("appliance-001", request.Directive.Endpoint.EndpointId);
            Assert.NotNull(request.Directive.Endpoint.Scope);
            Assert.Equal("BearerToken", request.Directive.Endpoint.Scope.Type);
        }

        [Fact]
        public void Can_Deserialize_BrightnessController_SetBrightness_Request()
        {
            var request = Utility.ExampleFileContent<SmartHomeRequest>("SmartHomeBrightnessControllerSet.json");

            Assert.NotNull(request);
            Assert.Equal("Alexa.BrightnessController", request.Directive.Header.Namespace);
            Assert.Equal("SetBrightness", request.Directive.Header.Name);
            
            var payload = JsonConvert.DeserializeObject<BrightnessControllerSetPayload>(request.Directive.Payload.ToString());
            Assert.NotNull(payload);
            Assert.Equal(75, payload.Brightness);
        }

        [Fact]
        public void Can_Deserialize_Discovery_Response()
        {
            var response = Utility.ExampleFileContent<SmartHomeResponse>("SmartHomeDiscoveryResponse.json");

            Assert.NotNull(response);
            Assert.NotNull(response.Event);
            Assert.Equal("Alexa.Discovery", response.Event.Header.Namespace);
            Assert.Equal("Discover.Response", response.Event.Header.Name);
            Assert.Equal("3", response.Event.Header.PayloadVersion);

            var payload = JsonConvert.DeserializeObject<Response.SmartHome.DiscoveryResponsePayload>(response.Event.Payload.ToString());
            Assert.NotNull(payload);
            Assert.NotNull(payload.Endpoints);
            Assert.Single(payload.Endpoints);

            var endpoint = payload.Endpoints[0];
            Assert.Equal("appliance-001", endpoint.EndpointId);
            Assert.Equal("Sample Manufacturer", endpoint.ManufacturerName);
            Assert.Equal("Living Room Light", endpoint.FriendlyName);
            Assert.Contains("LIGHT", endpoint.DisplayCategories);
            Assert.NotNull(endpoint.Capabilities);
            Assert.Equal(3, endpoint.Capabilities.Count);
        }

        [Fact]
        public void Can_Deserialize_Discovery_Response_With_Capabilities()
        {
            var response = Utility.ExampleFileContent<SmartHomeResponse>("SmartHomeDiscoveryResponse.json");
            var payload = JsonConvert.DeserializeObject<Response.SmartHome.DiscoveryResponsePayload>(response.Event.Payload.ToString());
            var endpoint = payload.Endpoints[0];

            var powerCapability = endpoint.Capabilities.FirstOrDefault(c => c.Interface == "Alexa.PowerController");
            Assert.NotNull(powerCapability);
            Assert.Equal("AlexaInterface", powerCapability.Type);
            Assert.Equal("3", powerCapability.Version);
            Assert.NotNull(powerCapability.Properties);
            Assert.True(powerCapability.Properties.ProactivelyReported);
            Assert.True(powerCapability.Properties.Retrievable);
            Assert.Single(powerCapability.Properties.Supported);
            Assert.Equal("powerState", powerCapability.Properties.Supported[0].Name);

            var brightnessCapability = endpoint.Capabilities.FirstOrDefault(c => c.Interface == "Alexa.BrightnessController");
            Assert.NotNull(brightnessCapability);
            Assert.Equal("brightness", brightnessCapability.Properties.Supported[0].Name);
        }

        [Fact]
        public void Can_Deserialize_PowerController_Response_With_Context()
        {
            var response = Utility.ExampleFileContent<SmartHomeResponse>("SmartHomePowerControllerResponse.json");

            Assert.NotNull(response);
            Assert.NotNull(response.Event);
            Assert.Equal("Alexa", response.Event.Header.Namespace);
            Assert.Equal("Response", response.Event.Header.Name);
            Assert.NotNull(response.Event.Header.CorrelationToken);

            Assert.NotNull(response.Context);
            Assert.NotNull(response.Context.Properties);
            Assert.Single(response.Context.Properties);

            var property = response.Context.Properties[0];
            Assert.Equal("Alexa.PowerController", property.Namespace);
            Assert.Equal("powerState", property.Name);
            Assert.Equal("ON", property.Value.ToString());
            Assert.Equal(500, property.UncertaintyInMilliseconds);
        }

        [Fact]
        public void Can_Deserialize_Error_Response()
        {
            var response = Utility.ExampleFileContent<SmartHomeResponse>("SmartHomeErrorResponse.json");

            Assert.NotNull(response);
            Assert.NotNull(response.Event);
            Assert.Equal("Alexa", response.Event.Header.Namespace);
            Assert.Equal("ErrorResponse", response.Event.Header.Name);

            var payload = JsonConvert.DeserializeObject<SmartHomeErrorPayload>(response.Event.Payload.ToString());
            Assert.NotNull(payload);
            Assert.Equal("ENDPOINT_UNREACHABLE", payload.Type);
            Assert.Equal("Unable to reach endpoint appliance-001", payload.Message);
        }

        [Fact]
        public void Can_Serialize_Discovery_Request()
        {
            var request = new SmartHomeRequest
            {
                Directive = new SmartHomeDirective
                {
                    Header = new SmartHomeHeader
                    {
                        Namespace = SmartHomeNamespaces.Discovery,
                        Name = SmartHomeDirectiveNames.Discover,
                        MessageId = Guid.NewGuid().ToString(),
                        PayloadVersion = "3"
                    },
                    Payload = new Request.SmartHome.DiscoveryRequestPayload
                    {
                        Scope = new Scope
                        {
                            Type = "BearerToken",
                            Token = "test-token"
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(request);
            Assert.Contains("Alexa.Discovery", json);
            Assert.Contains("Discover", json);
            Assert.Contains("test-token", json);
        }

        [Fact]
        public void Can_Serialize_Discovery_Response()
        {
            var response = new SmartHomeResponse
            {
                Event = new SmartHomeEvent
                {
                    Header = new SmartHomeHeader
                    {
                        Namespace = SmartHomeNamespaces.Discovery,
                        Name = "Discover.Response",
                        MessageId = Guid.NewGuid().ToString(),
                        PayloadVersion = "3"
                    },
                    Payload = new Response.SmartHome.DiscoveryResponsePayload
                    {
                        Endpoints = new System.Collections.Generic.List<DiscoveryEndpoint>
                        {
                            new DiscoveryEndpoint
                            {
                                EndpointId = "test-001",
                                ManufacturerName = "Test Manufacturer",
                                FriendlyName = "Test Device",
                                Description = "A test device",
                                DisplayCategories = new System.Collections.Generic.List<string> { "LIGHT" },
                                Capabilities = new System.Collections.Generic.List<Capability>
                                {
                                    new Capability
                                    {
                                        Type = "AlexaInterface",
                                        Interface = SmartHomeNamespaces.PowerController,
                                        Version = "3",
                                        Properties = new CapabilityProperties
                                        {
                                            Supported = new System.Collections.Generic.List<CapabilityPropertyName>
                                            {
                                                new CapabilityPropertyName { Name = "powerState" }
                                            },
                                            ProactivelyReported = true,
                                            Retrievable = true
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(response);
            Assert.Contains("test-001", json);
            Assert.Contains("Test Device", json);
            Assert.Contains("powerState", json);
        }
    }
}
