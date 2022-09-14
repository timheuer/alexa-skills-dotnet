# Alexa Skills SDK for .NET


Read this document in [Español](/docs/README_es.md)

[![Open in Visual Studio Code](https://open.vscode.dev/badges/open-in-vscode.svg)](https://github.dev/timheuer/alexa-skills-dotnet)

![.NET Core Build and Deploy](https://github.com/timheuer/alexa-skills-dotnet/workflows/.NET%20Core%20Build%20and%20Deploy/badge.svg?branch=master)
[![All Contributors](https://img.shields.io/badge/all_contributors-16-orange.svg?style=flat-square)](#contributors)
[![Sponsor Me](https://img.shields.io/badge/Sponsor%20Me%20and%20My%20Projects!--lightgre?style=social&logo=github)](https://github.com/sponsors/timheuer)

Alexa.NET is a helper library for working with Alexa skill requests/responses in C#.  Whether you are using the AWS Lambda service or hosting your own service on your server, this library aims just to make working with the Alexa API more natural for a C# developer using a strongly-typed object model.

Alexa.NET also serves as a base foundation for a set of further Alexa skill development extensions from [Steven Pears](https://github.com/stoiveyp):

* Management [GitHub](https://github.com/stoiveyp/Alexa.NET.Management) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Management)
* In-skill Pricing [GitHub](https://github.com/stoiveyp/Alexa.NET.InSkillPricing) / [NuGet](https://www.nuget.org/packages/Alexa.NET.InSkillPricing)
* Messaging [GitHub](https://github.com/stoiveyp/Alexa.NET.SkillMessaging) / [NuGet](https://www.nuget.org/packages/Alexa.NET.SkillMessaging)
* Gadgets [GitHub](https://github.com/stoiveyp/Alexa.NET.Gadgets) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Gadgets)
* Customer and Person Profile API [GitHub](https://github.com/stoiveyp/Alexa.NET.Profile) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Profile)
* Settings API [GitHub](https://github.com/stoiveyp/Alexa.NET.Settings) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Settings)
* APL Support [GitHub](https://github.com/stoiveyp/Alexa.NET.APL) / [NuGet](https://www.nuget.org/packages/Alexa.NET.APL)
* Reminders API [GitHub](https://github.com/stoiveyp/Alexa.NET.Reminders) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Reminders)
* Proactive Events API [GitHub](https://github.com/stoiveyp/Alexa.NET.ProactiveEvents) / [NuGet](https://www.nuget.org/packages/Alexa.NET.ProactiveEvents)
* CanFulfillIntent Request Support [GitHub](https://github.com/stoiveyp/Alexa.NET.CanFulfillIntentRequest) / [NuGet](https://www.nuget.org/packages/Alexa.NET.CanFulfillIntentRequest)
* Response Assertions [GitHub](https://github.com/stoiveyp/Alexa.NET.TestUtility/tree/master/Alexa.NET.Assertions) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Assertions)
* SkillFlow support (experimental)
    * Object Model [GitHub](https://github.com/stoiveyp/Alexa.NET.SkillFlow) / [NuGet](https://www.nuget.org/packages/Alexa.NET.SkillFlow)
    * Interpreter [GitHub](https://github.com/stoiveyp/Alexa.NET.SkillFlow.Interpreter) / [NuGet](https://www.nuget.org/packages/Alexa.NET.SkillFlow.Interpreter)
    * Text Generator [GitHub](https://github.com/stoiveyp/Alexa.NET.SkillFlow.TextGenerator) / [NuGet](https://www.nuget.org/packages/Alexa.NET.SkillFlow.TextGenerator)
    * Alexa.NET Code Generation Tool [GitHub](https://github.com/stoiveyp/Alexa.NET.SkillFlow.CodeGenerator) / [NuGet](https://www.nuget.org/packages/Alexa.NET.SkillFlow.Tool)
* Timers API [GitHub](https://github.com/stoiveyp/Alexa.NET.Timers) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Timers)
* Web API for Games [GitHub](https://github.com/stoiveyp/Alexa.NET.WebApiGames) / [NuGet](https://www.nuget.org/packages/Alexa.NET.WebApiGames)
* Shopping Kit [GitHub](https://github.com/stoiveyp/Alexa.NET.ShoppingActions) / [NuGet](https://www.nuget.org/packages/Alexa.NET.ShoppingActions)
* Conversations API (Beta) [GitHub](https://github.com/stoiveyp/Alexa.NET.Conversations) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Conversations)
* Pin Confirmation (Beta) [GitHub](https://github.com/stoiveyp/Alexa.NET.PinConfirmation) / [NuGet](https://www.nuget.org/packages/Alexa.NET.PinConfirmation)

# Setup
Regardless of your architecture, your function for Alexa will be accepting a SkillRequest and returning a SkillResponse. The deserialization of the incoming request into a SkillRequest object will depend on your framework.
```csharp
public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
{
    // your function logic goes here
    return new SkillResponse("OK");
}
```

## Serialization
Use the `Amazon.Lambda.Serialization.Json` package. The default may be different depending on how you created your project.

In your project file:
```csproj
<Project Sdk="Microsoft.NET.Sdk">
  <!-- ... -->
  <ItemGroup>
    <PackageReference Include="Alexa.NET" Version="1.15.0" />
    <PackageReference Include="Amazon.Lambda.Core" Version="1.2.0" />
    <PackageReference Include="Amazon.Lambda.Serialization.Json" Version="1.8.0" />
  </ItemGroup>
</Project>
```

In any .cs file:
```csharp
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
```

# Table of Contents
- [Contributors](#contributors)
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

## Contributors

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="http://www.imacode.ninja"><img src="https://avatars3.githubusercontent.com/u/147125?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Steven Pears</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=stoiveyp" title="Code">💻</a> <a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=stoiveyp" title="Documentation">📖</a> <a href="#tutorial-stoiveyp" title="Tutorials">✅</a> <a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=stoiveyp" title="Tests">⚠️</a></td>
    <td align="center"><a href="http://vineetyadav.com"><img src="https://avatars1.githubusercontent.com/u/7949851?v=4?s=100" width="100px;" alt=""/><br /><sub><b>yadavvineet</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=yadavvineet" title="Code">💻</a></td>
    <td align="center"><a href="https://www.linkedin.com/in/soc-sieng-b45a9473/"><img src="https://avatars1.githubusercontent.com/u/2647062?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Soc Sieng</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=socsieng" title="Documentation">📖</a></td>
    <td align="center"><a href="http://www.stuartbuchanan.co.uk"><img src="https://avatars3.githubusercontent.com/u/16162689?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Stuart Buchanan</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=fuzzysb" title="Tests">⚠️</a></td>
    <td align="center"><a href="https://github.com/rdlaitila"><img src="https://avatars0.githubusercontent.com/u/1124388?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Regan Laitila</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=rdlaitila" title="Code">💻</a></td>
    <td align="center"><a href="https://github.com/IcanBENCHurCAT"><img src="https://avatars1.githubusercontent.com/u/4152550?v=4?s=100" width="100px;" alt=""/><br /><sub><b>IcanBENCHurCAT</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=IcanBENCHurCAT" title="Documentation">📖</a></td>
    <td align="center"><a href="https://www.ydeho.com/"><img src="https://avatars3.githubusercontent.com/u/29730840?v=4?s=100" width="100px;" alt=""/><br /><sub><b>VinceGusmini</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=VinceGusmini" title="Code">💻</a></td>
  </tr>
  <tr>
    <td align="center"><a href="http://www.yasoon.com"><img src="https://avatars2.githubusercontent.com/u/2111803?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Tobias Viehweger</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=tobiasviehweger" title="Code">💻</a></td>
    <td align="center"><a href="http://matthiasshapiro.com"><img src="https://avatars3.githubusercontent.com/u/235365?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Matthias Shapiro</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=matthiasxc" title="Code">💻</a> <a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=matthiasxc" title="Tests">⚠️</a></td>
    <td align="center"><a href="http://techpreacher.corti.com"><img src="https://avatars1.githubusercontent.com/u/841949?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Sascha Corti</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=TechPreacher" title="Code">💻</a></td>
    <td align="center"><a href="https://github.com/StenmannsAr"><img src="https://avatars3.githubusercontent.com/u/27204921?v=4?s=100" width="100px;" alt=""/><br /><sub><b>StenmannsAr</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=StenmannsAr" title="Code">💻</a></td>
    <td align="center"><a href="https://adriangodong.com"><img src="https://avatars3.githubusercontent.com/u/1140137?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Adrian Godong</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=adriangodong" title="Code">💻</a></td>
    <td align="center"><a href="http://jad.codes"><img src="https://avatars0.githubusercontent.com/u/68933?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Jaddie</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/issues?q=author%3Ajaddie" title="Bug reports">🐛</a></td>
    <td align="center"><a href="https://github.com/evgeni-nabokov"><img src="https://avatars3.githubusercontent.com/u/3168823?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Evgeni Nabokov</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=evgeni-nabokov" title="Code">💻</a></td>
  </tr>
  <tr>
    <td align="center"><a href="https://dev.to/mteheran"><img src="https://avatars0.githubusercontent.com/u/3578356?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Miguel Teheran</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=Mteheran" title="Documentation">📖</a></td>
    <td align="center"><a href="https://martincostello.com/"><img src="https://avatars0.githubusercontent.com/u/1439341?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Martin Costello</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=martincostello" title="Code">💻</a></td>
    <td align="center"><a href="https://github.com/shinya-terasaki"><img src="https://avatars2.githubusercontent.com/u/16497603?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Shinya Terasaki</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=shinya-terasaki" title="Code">💻</a></td>
    <td align="center"><a href="https://github.com/bcuff"><img src="https://avatars1.githubusercontent.com/u/504266?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Brandon</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=bcuff" title="Documentation">📖</a></td>
    <td align="center"><a href="https://github.com/aadupirn"><img src="https://avatars1.githubusercontent.com/u/16725484?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Aadu Pirn</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=aadupirn" title="Code">💻</a></td>
    <td align="center"><a href="https://peterfoot.net"><img src="https://avatars.githubusercontent.com/u/3985053?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Peter Foot</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=peterfoot" title="Code">💻</a></td>
    <td align="center"><a href="https://twitter.com/bnachawati"><img src="https://avatars.githubusercontent.com/u/34027178?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Benoit</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=nachawat" title="Tests">⚠️</a> <a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=nachawat" title="Code">💻</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

## Code of Conduct
This project has adopted the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct). For more information see the Code of Conduct itself or contact project maintainers with any additional questions or comments or to report a violation.
