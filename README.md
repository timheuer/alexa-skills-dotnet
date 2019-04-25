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
* Proactive Events API [GitHub](https://github.com/stoiveyp/Alexa.NET.ProactiveEvents) / [NuGet](https://www.nuget.org/packages/Alexa.NET.ProactiveEvents)
* CanFulfillIntent Request Support [GitHub](https://github.com/stoiveyp/Alexa.NET.CanFulfillIntentRequest) / [NuGet](https://www.nuget.org/packages/Alexa.NET.CanFulfillIntentRequest)

# Setup
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
   * [AccountLinkSkillEventRequest](#handling-the-accountlinkskilleventrequest)
   * [AudioPlayerRequest](#handling-the-audioplayerrequest)
   * [DisplayElementSelectedRequest](#handling-the-displayelementselectedrequest)
   * [IntentRequest](#handling-the-intentrequest)
   * [LaunchRequest](#handling-the-launchrequest)
   * [PermissionSkillEventRequest](#handling-the-permissionskilleventrequest)
   * [PlaybackControllerRequest](#handling-the-playbackcontrollerrequest)
   * [SessionEndedRequest](#handling-the-sessionendedrequest)
   * [SkillEventRequest](#handling-the-skilleventrequest)
   * [SystemExceptionRequest](#handling-the-systemexceptionrequest)
- [Responses](#responses)
    * [Ask vs. Tell](#ask-vs-tell)
    * [SSML Response](#ssml-response)
    * [SSML Response With Card](#ssml-response-with-card)
    * [SSML Response With Reprompt](#ssml-response-with-reprompt)
- [Session Variables](#session-variables)
- [Responses Without Helpers](#responses-without-helpers)
- [Progressive Responses](#progressive-responses)


# Request Types
Alexa will send different types of requests depending on the events you should respond to. Below are all of the types of requests:

- ```AccountLinkSkillEventRequest```
- ```AudioPlayerRequest```
- ```DisplayElementSelectedRequest```
- ```IntentRequest```
- ```LaunchRequest```
- ```PermissionSkillEventRequest```
- ```PlaybackControllerRequest```
- ```SessionEndedRequest```
- ```SkillEventRequest```
- ```SystemExceptionRequest```

## Handling the AccountLinkSkillEventRequest
This request is used for linking Alexa to another account. The request will come with the access token needed to interact with the connected service. These events are only sent if they have been subscribed to.
```csharp
var accountLinkReq = input.Request as AccountLinkSkillEventRequest;
var accessToken = accountLinkReq.AccessToken;
```

## Handling the AudioPlayerRequest
Audio Player Requests will be sent when a skill is supposed to play audio, or if an audio state change has occured on the device.
```csharp
// do some audio response stuff
var audioRequest = input.Request as AudioPlayerRequest;

if (audioRequest.AudioRequestType == AudioRequestType.PlaybackNearlyFinished)
{
    // queue up another audio file
}
```

### AudioRequestType
Each ```AudioPlayerRequest``` will also come with a request type to describe the state change:
- ```PlaybackStarted```
- ```PlaybackFinished```
- ```PlaybackStopped```
- ```PlaybackNearlyFinished```
- ```PlaybackFailed```

## Handling the DisplayElementSelectedRequest
Display Element Selected Requests will be sent when a skill has a GUI, and one of the buttons were selected by the user. This request comes with a token that will tell you which GUI element was selected.
```csharp
var elemSelReq = input.Request as DisplayElementSelectedRequest;
var buttonID = elemSelReq.Token;
```

## Handling the IntentRequest
This is the type that will likely be used most often. IntentRequest will also come with an ```Intent``` object and a ```DialogState``` of either ```STARTED```, ```IN_PROGRESS``` or ```COMPLETED```

### Intent
Each intent is defined by the name configured in the Alexa Developer Console. If you have included slots in your intent, they will be included in this object, along with a confirmation status.
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

## Handling the LaunchRequest
This type of request is sent when your skill is opened with no intents triggered. You should respond and expect an ```IntentRequest``` to follow.
```csharp
if(input.Request is LaunchRequest)
{
   return ResponseBuilder.Ask("How can I help you today?");
}

```

## Handling the PermissionSkillEventRequest
This event is sent when a customer grants or revokes permissions. This request will include a ```SkillEventPermissions``` object with the included permission changes. These events are only sent if they have been subscribed to.
```csharp
var permissionReq = input.Request as PermissionSkillEventRequest;
var firstPermission = permissionReq.Body.AcceptedPermissions[0];
```

## Handling the PlaybackControllerRequest
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

## Handling the SessionEndedRequest
This event is sent if the user requests to exit, their response takes too long or an error has occured on the device.
```csharp
var sessEndReq = input.Request as SessionEndedRequest;
switch(sessEndReq.Reason)
{
   case Reason.UserInitiated:
      break;
   case Reason.Error:
      break;
   case Reason.ExceededMaxReprompts:
      break;
}
```

## Handling the SkillEventRequest
This event is sent when a user enables or disables the skill. These events are only sent if they have been subscribed to.

## Handling the SystemExceptionRequest
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

# Responses

## Ask Vs Tell
There are two helper methods for forming a speech response with ```ResponseBuilder```:
```csharp
var finalResponse = ResponseBuilder.Tell("We are done here.");
var openEndedResponse = ResponseBuilder.Ask("Are we done here?");
```
Using Tell sets ```ShouldEndSession``` to ```true```. Using Ask sets ```ShouldEndSession``` to ```false```. Use the appropriate function depending on whether you expect to continue dialog or not.

## SSML Response
SSML can be used to customize the way Alexa speaks. Below is an example of using SSML with the helper functions:
```csharp
// build the speech response 
var speech = new SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.<break strength=\"x-strong\"/>I hope you have a good day.</speak>";

// create the response using the ResponseBuilder
var finalResponse = ResponseBuilder.Tell(speech);
return finalResponse;
```

## SSML Response With Card
In your response you can also have a 'Card' response, which presents UI elements to Alexa. ```ResponseBuilder``` presently builds Simple cards only, which contain titles and plain text.
```csharp
// create the speech response - cards still need a voice response
var speech = new SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// create the card response
var finalResponse = ResponseBuilder.TellWithCard(speech, "Your Card Title", "Your card content text goes here, no HTML formatting honored");
return finalResponse;

```

## SSML Response With Reprompt
If you want to reprompt the user, use the Ask helpers. A reprompt can be useful if you would like to continue the conversation, or if you would like to remind the user to answer the question.
```csharp
// create the speech response
var speech = new SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// create the speech reprompt
var repromptMessage = new PlainTextOutputSpeech();
repromptMessage.Text = "Would you like to know what tomorrow is?";

// create the reprompt
var repromptBody = new Reprompt();
repromptBody.OutputSpeech = repromptMessage;

// create the response
var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
return finalResponse;
```

## Play Audio File
If your skill is registered as an audio player, you can send directives (instructions to play, enqueue, or stop an audio stream). 

```csharp
// create the speech response - you most likely will still have this
string audioUrl = "http://mydomain.com/myaudiofile.mp3";
string audioToken = "a token to describe the audio file"; 

var audioResponse = ResponseBuilder.AudioPlayerPlay(PlayBehavior.ReplaceAll, audioUrl, audioToken);

return audioResponse
```

# Session Variables
Session variables can be saved into a response, and will be sent back and forth as long as the session remains open.

## Response with session variable
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

# Responses Without Helpers
If you do not want to use the helper Tell/Ask functions, you can build up the response manually using the ```Response``` and ```IOutputSpeech``` objects. If you would like to include a ```StandardCard``` or ```LinkAccountCard``` in your response, you could add it like this onto the response body:
```csharp
// create the speech response
var speech = new SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// create the reprompt speech
var repromptMessage = new PlainTextOutputSpeech();
repromptMessage.Text = "Would you like to know what tomorrow is?";

// create the reprompt object
var repromptBody = new Reprompt();
repromptBody.OutputSpeech = repromptMessage;

// create the response
var responseBody = new ResponseBody();
responseBody.OutputSpeech = speech;
responseBody.ShouldEndSession = false; // this triggers the reprompt
responseBody.Reprompt = repromptBody;
responseBody.Card = new SimpleCard {Title = "Test", Content = "Testing Alexa"};

var skillResponse = new SkillResponse();
skillResponse.Response = responseBody;
skillResponse.Version = "1.0";

return skillResponse;
```
# Progressive Responses
Your skill can send progressive responses to keep the user engaged while your skill prepares a full response to the user's request. Below is an example of sending a progressive response:
```csharp
var progressiveResponse = new ProgressiveResponse(skillRequest);
progressiveResponse.SendSpeech("Please wait while I gather your data.");
```

# Community Contributions
Alexa.NET has grown thanks to many suggestions, bug fixes, and direct contributions to the project here.  Be sure to give them thanks:

- Steven Pears [@stevenpears](https://twitter.com/stevenpears) - tons of code contributions and extensions
- [IcanBENCHurCAT](https://github.com/IcanBENCHurCAT) - documentation
