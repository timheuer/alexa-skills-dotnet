# Alexa Smart Home API Support

This library includes support for Amazon Alexa Smart Home API v3, enabling you to create smart home skills that control devices like lights, switches, thermostats, and more.

## Overview

The Smart Home API classes are organized into two namespaces:
- `Alexa.NET.Request.SmartHome` - Request and directive structures
- `Alexa.NET.Response.SmartHome` - Response and event structures

## Key Classes

### Request Structure
- **SmartHomeRequest** - Top-level request wrapper containing a directive
- **SmartHomeDirective** - Contains header, endpoint, and payload
- **SmartHomeHeader** - Metadata including namespace, name, messageId, and payloadVersion
- **SmartHomeEndpoint** - Device endpoint with ID and scope
- **Scope** - Authorization scope with bearer token

### Response Structure
- **SmartHomeResponse** - Top-level response wrapper containing an event
- **SmartHomeEvent** - Contains header, endpoint, and payload
- **SmartHomeContext** - State reporting with properties collection
- **SmartHomeProperty** - Individual property with namespace, name, value, and timestamp

### Discovery
- **DiscoveryRequestPayload** - Discovery directive payload
- **DiscoveryResponsePayload** - Discovery response with endpoints
- **DiscoveryEndpoint** - Device endpoint definition with capabilities
- **Capability** - Capability definition for device interfaces
- **CapabilityProperties** - Supported properties configuration

### Controller Payloads
- **PowerControllerPayload** - For TurnOn/TurnOff directives
- **BrightnessControllerSetPayload** - For SetBrightness directive
- **BrightnessControllerAdjustPayload** - For AdjustBrightness directive

### Error Handling
- **SmartHomeErrorPayload** - Error response payload
- **SmartHomeErrorTypes** - Constants for standard error types

### Constants
- **SmartHomeNamespaces** - Common namespace constants
- **SmartHomeDirectiveNames** - Common directive name constants

## Usage Examples

### Handling Discovery Requests

```csharp
using Alexa.NET.Request.SmartHome;
using Alexa.NET.Response.SmartHome;
using Newtonsoft.Json;

// Deserialize incoming request
var request = JsonConvert.DeserializeObject<SmartHomeRequest>(requestJson);

if (request.Directive.Header.Namespace == SmartHomeNamespaces.Discovery &&
    request.Directive.Header.Name == SmartHomeDirectiveNames.Discover)
{
    // Create discovery response
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
            Payload = new DiscoveryResponsePayload
            {
                Endpoints = new List<DiscoveryEndpoint>
                {
                    new DiscoveryEndpoint
                    {
                        EndpointId = "light-001",
                        ManufacturerName = "Your Company",
                        FriendlyName = "Living Room Light",
                        Description = "Smart Light",
                        DisplayCategories = new List<string> { "LIGHT" },
                        Capabilities = new List<Capability>
                        {
                            new Capability
                            {
                                Type = "AlexaInterface",
                                Interface = SmartHomeNamespaces.PowerController,
                                Version = "3",
                                Properties = new CapabilityProperties
                                {
                                    Supported = new List<CapabilityPropertyName>
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

    return JsonConvert.SerializeObject(response);
}
```

### Handling PowerController Directives

```csharp
// Handle TurnOn/TurnOff
if (request.Directive.Header.Namespace == SmartHomeNamespaces.PowerController)
{
    var endpointId = request.Directive.Endpoint.EndpointId;
    var isTurnOn = request.Directive.Header.Name == SmartHomeDirectiveNames.TurnOn;
    
    // Control your device here...
    ControlDevice(endpointId, isTurnOn);
    
    // Create response with state reporting
    var response = new SmartHomeResponse
    {
        Event = new SmartHomeEvent
        {
            Header = new SmartHomeHeader
            {
                Namespace = SmartHomeNamespaces.Alexa,
                Name = SmartHomeDirectiveNames.Response,
                MessageId = Guid.NewGuid().ToString(),
                PayloadVersion = "3",
                CorrelationToken = request.Directive.Header.CorrelationToken
            },
            Endpoint = new SmartHomeEndpoint
            {
                EndpointId = endpointId,
                Scope = request.Directive.Endpoint.Scope
            },
            Payload = new object()
        },
        Context = new SmartHomeContext
        {
            Properties = new List<SmartHomeProperty>
            {
                new SmartHomeProperty
                {
                    Namespace = SmartHomeNamespaces.PowerController,
                    Name = "powerState",
                    Value = isTurnOn ? "ON" : "OFF",
                    TimeOfSample = DateTime.UtcNow,
                    UncertaintyInMilliseconds = 500
                }
            }
        }
    };

    return JsonConvert.SerializeObject(response);
}
```

### Handling BrightnessController Directives

```csharp
if (request.Directive.Header.Namespace == SmartHomeNamespaces.BrightnessController)
{
    var endpointId = request.Directive.Endpoint.EndpointId;
    
    if (request.Directive.Header.Name == SmartHomeDirectiveNames.SetBrightness)
    {
        var payload = JsonConvert.DeserializeObject<BrightnessControllerSetPayload>(
            request.Directive.Payload.ToString());
        
        // Set device brightness
        SetBrightness(endpointId, payload.Brightness);
    }
    else if (request.Directive.Header.Name == SmartHomeDirectiveNames.AdjustBrightness)
    {
        var payload = JsonConvert.DeserializeObject<BrightnessControllerAdjustPayload>(
            request.Directive.Payload.ToString());
        
        // Adjust device brightness
        AdjustBrightness(endpointId, payload.BrightnessDelta);
    }
    
    // Return response with updated state...
}
```

### Returning Error Responses

```csharp
// When an error occurs
var errorResponse = new SmartHomeResponse
{
    Event = new SmartHomeEvent
    {
        Header = new SmartHomeHeader
        {
            Namespace = SmartHomeNamespaces.Alexa,
            Name = "ErrorResponse",
            MessageId = Guid.NewGuid().ToString(),
            PayloadVersion = "3",
            CorrelationToken = request.Directive.Header.CorrelationToken
        },
        Endpoint = new SmartHomeEndpoint
        {
            EndpointId = request.Directive.Endpoint.EndpointId
        },
        Payload = new SmartHomeErrorPayload
        {
            Type = SmartHomeErrorTypes.EndpointUnreachable,
            Message = "Unable to reach the device"
        }
    }
};

return JsonConvert.SerializeObject(errorResponse);
```

## Supported Interfaces

The library provides constants for the following Alexa Smart Home interfaces:

- **Alexa.Discovery** - Device discovery
- **Alexa.PowerController** - On/Off control
- **Alexa.BrightnessController** - Brightness control
- **Alexa.ColorController** - Color control
- **Alexa.ColorTemperatureController** - Color temperature
- **Alexa.PercentageController** - Percentage-based control
- **Alexa.ThermostatController** - Thermostat control
- **Alexa.LockController** - Lock control
- **Alexa.SceneController** - Scene activation
- **Alexa.ChannelController** - Channel control
- **Alexa.InputController** - Input selection
- **Alexa.Speaker** - Volume control
- **Alexa.StepSpeaker** - Step-based volume
- **Alexa.PlaybackController** - Media playback
- **Alexa.EndpointHealth** - Health reporting

## Additional Resources

- [Alexa Smart Home API Documentation](https://developer.amazon.com/en-US/docs/alexa/device-apis/smart-home-general-apis.html)
- [Alexa Discovery Interface](https://developer.amazon.com/en-US/docs/alexa/device-apis/alexa-discovery.html)
- [Alexa PowerController Interface](https://developer.amazon.com/en-US/docs/alexa/device-apis/alexa-powercontroller.html)
- [Alexa BrightnessController Interface](https://developer.amazon.com/en-US/docs/alexa/device-apis/alexa-brightnesscontroller.html)
