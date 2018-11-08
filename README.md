# Alexa Skills SDK for .NET

![Build status](https://timheuer.visualstudio.com/_apis/public/build/definitions/01aba697-af87-48c2-8bde-627fb90746d8/2/badge)

Alexa.NET is a helper library for working with Alexa skill requests/responses in C#.  Whether you are using the AWS Lambda service or hosting your own service on your server, this library aims just to make working with the Alexa API more natural for a C# developer using a strongly-typed object model.

Alexa.NET also serves as a base foundation for a set of further Alexa skill development extensions from [Steven Pears](https://github.com/stoiveyp):

* Management [GitHub](https://github.com/stoiveyp/Alexa.NET.Management) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Management)
* Notifications [GitHub](https://github.com/stoiveyp/Alexa.NET.Notifications) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Notifications)
* In-skill Pricing [GitHub](https://github.com/stoiveyp/Alexa.NET.InSkillPricing) / [NuGet](https://www.nuget.org/packages/Alexa.NET.InSkillPricing)
* Messaging [GitHub](https://github.com/stoiveyp/Alexa.NET.SkillMessaging) / [NuGet](https://www.nuget.org/packages/Alexa.NET.SkillMessaging)
* Gadgets [GitHub](https://github.com/stoiveyp/Alexa.NET.Gadgets) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Gadgets)
* Customer Profile API [GitHub](https://github.com/stoiveyp/Alexa.NET.CustomerProfile) / [NuGet](https://www.nuget.org/packages/Alexa.NET.CustomerProfile)
* Settings API [GitHub](https://github.com/stoiveyp/Alexa.NET.Settings) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Settings)
* APL Support [GitHub](https://github.com/stoiveyp/Alexa.NET.APL) / [NuGet](https://www.nuget.org/packages/Alexa.NET.APL)
* Reminders API [GitHub](https://github.com/stoiveyp/Alexa.NET.Reminders) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Reminders)

## Setup
Regardless of your architecture, your function for Alexa will be accepting a SkillRequest and returning a SkillResponse. The deserialization of the incoming request into a SkillRequest object will depend on your framework.
```csharp
public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
{
    // your function logic goes here
    return new SkillResponse("OK");
}
```

# Table of Contents
- [Request Types](#request-types)    
   * [Account Link Skill Event Request](#accountlinkskilleventrequest)
   * [Audio Player Request](#audio-player-request)
   * [Display Element Selected Request](#displayelementselectedrequest)
   * [Intent Request](#intentrequest)
   * [Launch Request](#launchrequest)
   * [Permission Skill Event Request](#permissionskilleventrequest)
   * [Playback Controller Request](#playbackcontrollerrequest)
   * [Session Ended Request](#sessionendedrequest)
   * [Skill Event Request](#skilleventrequest)
   * [System Exception Request](#systemexceptionrequest)
-[Responses](#responses)
-[Reprompt](#reprompt)


### Request-Types
Alexa will send different types of requests depending on what the user requested. Below are all of the types of requests:

- AccountLinkSkillEventRequest
- AudioPlayerRequest
- DisplayElementSelectedRequest
- IntentRequest
- LaunchRequest
- PermissionSkillEventRequest
- PlaybackControllerRequest
- SessionEndedRequest
- SkillEventRequest
- SystemExceptionRequest

```csharp
// check what type of a request it is
if (input.Request is IntentRequest)
{
    // do some intent-based stuff
}
else if (input.Request is LaunchRequest)
{
    // default launch path executed
}
else if (input.Request is AudioPlayerRequest)
{
    // do some audio response stuff
}
```

### AccountLinkSkillEventRequest
This request is used for linking Alexa to another account. The request will come with the access token needed to interact with the connected service.
```csharp
var accountLinkReq = input.Request as AccountLinkSkillEventRequest;
var accessToken = accountLinkReq.AccessToken;
```

### Audio-Player-Request
Audio Player Requests will be sent when a skill is supposed to play audio, or if an audio state change has occured on the device.

```csharp
// do some audio response stuff
var audioRequest = input.Request as AudioPlayerRequest;

if (audioRequest.AudioRequestType == AudioRequestType.PlaybackNearlyFinished)
{
    // queue up another audio file
}
```

## AudioRequestType
Each AudioPlayerRequest will also come with a request type. The following types are available:
- PlaybackStarted
- PlaybackFinished
- PlaybackStopped
- PlaybackNearlyFinished
- PlaybackFailed

### DisplayElementSelectedRequest
Display Element Selected Requests will be sent when a skill has a GUI, and one of the buttons were selected by the user. This request comes with a token that will tell you which GUI element was selected.
```csharp
var elemSelReq = input.Request as DisplayElementSelectedRequest;
var buttonID = elemSelReq.Token;
```

### IntentRequest
This is the type that will likely be used most often. IntentRequest will also come with an Intent object and a DialogState of either STARTED, IN_PROGRESS or COMPLETED

## Intent
Each intent is defined by the name configured in the Alexa Developer Console. If you have included slots in your intent, they will be included in this object, along with confirmation status.
```csharp
var intentRequest = input.Request as IntentRequest;

// check the name to determine what you should do
if (intentRequest.Intent.Name.Equals("MyIntentName"))
{
   if(intentRequest.DialogState.Equals("COMPLETED"))
   {
       // get the slots
       var firstValue = intentRequest.Intent.Slots["FirstSlot"].Value;
    }
}
```

### LaunchRequest
This type of request is sent when your skill is opened with no intents triggered. You should respond and expect an IntentRequest to follow.
```csharp
if(input.Request is LaunchRequest)
{
   return ResponseBuilder.Ask("How can I help you today?");
}

```

### PermissionSkillEventRequest
This event is sent when a customer grants or revokes permissions. This request will include a SkillEventPermissions object with the included permission changes.
```csharp
var permissionReq = input.Request as PermissionSkillEventRequest;
var firstPermission = permissionReq.Body.AcceptedPermissions[0];
```
### PlaybackControllerRequest
This event is sent to control playback for an audio player skill.
```csharp
var playbackReq = input.Request as PlaybackControllerRequest;
switch(playbackReq.PlaybackRequestType)
{
   case PlaybackControllerRequestType.Next:
      break;
   case PlaybackControllerRequestType.Pause:
      break;
   case PlaybackControllerRequestType.Play:
      break;
   case PlaybackControllerRequestType.Previous:
      break;
}
```
### SessionEndedRequest
This event is sent if the user requests to exit, times out or an error has occured on the device.
```csharp
var sessEndReq = input.Request as SessionEndedRequest;
switch(sessEndReq)
{
   case Reason.UserInitiated:
      break;
   case Reason.Error:
      break;
   case Reason.ExceededMaxReprompts:
      break;
}
```

### SkillEventRequest
This event is sent if a custom event has been configured in ASK CLI.

## SystemExceptionRequest
When an error occurs, whether as the result of a malformed event or too many requests, AVS will return a message to your client that includes an exception code and a description.
```csharp
var sysException = input.Request as SystemExceptionRequest;
string message = sysException.Error.Message;
string reqID = sysException.ErrorCause.requestId;
switch(sysException.Error.Type)
{
   case ErrorType.InvalidResponse:
      break;
   case ErrorType.DeviceCommunicationError:
      break;
   case ErrorType.InternalError:
      break;
   case ErrorType.MediaErrorUnknown:
      break;
   case ErrorType.InvalidMediaRequest:
      break;
   case ErrorType.MediaServiceUnavailable:
      break;
   case ErrorType.InternalServerError:
      break;
   case ErrorType.InternalDeviceError:
      break;
}
```

### Responses

## Ask vs. Tell
There are two base methods for forming a speech response with ResponseBuilder:
```csharp
var finalResponse = ResponseBuilder.Tell("We are done here.");
var openEndedResponse = ResponseBuilder.Ask("Are we done here?");
```
Using Tell sets ShouldEndSession to true. Using Ask sets ShouldEndSession to false. 

## Play an audio file

If your skill is registered as an audio player, you can send directives (instructions to play, enqueue, or stop an audio stream). 

```csharp
// create the speech response - you most likely will still have this
string audioUrl = "http://mydomain.com/myaudiofile.mp3";
string audioToken = "a token to describe the audio file"; 

var audioResponse = ResponseBuilder.AudioPlayerPlay(PlayBehavior.ReplaceAll, audioUrl, audioToken);

return audioResponse
```

## Build a simple voice response

There are various types of responses you can build and this library provides a helper function to build them up.  A simple one of having Alexa tell the user something using SSML may look like this:
```csharp
// build the speech response 
var speech = new Alexa.NET.Response.SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.<break strength=\"x-strong\"/>I hope you have a good day.</speak>";

// create the response using the ResponseBuilder
var finalResponse = ResponseBuilder.Tell(speech);
return finalResponse;
```

## Build a simple response with a Card
In your response you can also have a 'Card' response, which presents UI to the Alexa companion app for the registered user.  Cards presently are simple and contain basically titles and plain text (no HTML :-().  To create a response with cards, you can use the ResponseBuilder:
```csharp
// create the speech response - cards still need a voice response
var speech = new Alexa.NET.Response.SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// create the card response
var finalResponse = ResponseBuilder.TellWithCard(speech, "Your Card Title", "Your card content text goes here, no HTML formatting honored");
return finalResponse;

```

## Build a simple response with a reprompt
If you want to reprompt the user, use the Ask helpers
```csharp
// create the speech response
var speech = new Alexa.NET.Response.SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// create the speech reprompt
var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
repromptMessage.Text = "Would you like to know what tomorrow is?";

// create the reprompt
var repromptBody = new Alexa.NET.Response.Reprompt();
repromptBody.OutputSpeech = repromptMessage;

// create the response
var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
return finalResponse;
```

## Build a simple response with a session variable
```csharp
string speech = "The time is twelve twenty three.";
Session session = input.Session;

if(session.Attributes == null)
    session.Attributes = new Dictionary<string, object>();
session.Attributes["real_time"] = DateTime.Now;

return ResponseBuilder.Tell(speech, session);
```

## Retrieving session variable from request
```csharp
Session session = input.Session;
DateTime lastTime = session.Attributes["real_time"] as DateTime;

return ResponseBuilder.Tell("The last day you asked was at " + lastTime.DayOfWeek.ToString());
```

## Build a response without using helpers

If you do not want to use the helper Tell/Ask functions for the simple structure you 
can build up the response manually using the ```Response``` and some ```IOutputSpeech``` objects.
```csharp
// create the speech response - you most likely will still have this
var speech = new Alexa.NET.Response.SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// create the response
var responseBody = new Alexa.NET.Response.ResponseBody();
responseBody.OutputSpeech = speech;
responseBody.ShouldEndSession = true;

var skillResponse = new Alexa.NET.Response.SkillResponse();
skillResponse.Response = responseBody;
skillResponse.Version = "1.0";

return skillResponse;
```

## Build a reprompt without using helpers
To add reprompt to the response you just need to include that as well.
```csharp
// create the speech response - you most likely will still have this
var speech = new Alexa.NET.Response.SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// create the reprompt speech
var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
repromptMessage.Text = "Would you like to know what tomorrow is?";

// create the reprompt object
var repromptBody = new Alexa.NET.Response.Reprompt();
repromptBody.OutputSpeech = repromptMessage;

// create the response
var responseBody = new Alexa.NET.Response.ResponseBody();
responseBody.OutputSpeech = speech;
responseBody.ShouldEndSession = false; // this triggers the reprompt
responseBody.Reprompt = repromptBody;

var skillResponse = new Alexa.NET.Response.SkillResponse();
skillResponse.Response = responseBody;
skillResponse.Version = "1.0";

return skillResponse;
```
