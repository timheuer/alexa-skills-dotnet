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

# What about some documentation?
The below samples are the best documentation we have right now on how to use this library.  Of course, help is always appreciated if anyone desires to submit a pull request with more proper/readable documentation.  

# Some Quick Samples
Here are some *simple* examples of how to use this library assuming the default signature of the AWS Lambda C# function:
```csharp
public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
{
    // your function logic goes here
}
```
## Get the request type (Launch, Intent, Audio, etc)
You most likely are going to want to get the type of request to know if it was the default launch, an intent, or maybe an audio request.
```csharp
// check what type of a request it is like an IntentRequest or a LaunchRequest
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

## Get the intent and look at specifics
Once you know it is an IntentRequest you probably want to know which one (name) and perhaps pull out parameters (slots):
```csharp
// do some intent-based stuff
var intentRequest = input.Request as IntentRequest;

// check the name to determine what you should do
if (intentRequest.Intent.Name.Equals("MyIntentName"))
{
    // get the slots
    var firstValue = intentRequest.Intent.Slots["FirstSlot"].Value;
}
```

## Ask vs. Tell
There are two base methods for forming a response with ResponseBuilder:
```csharp
var finalResponse = ResponseBuilder.Tell("We are done here.");
var openEndedResponse = ResponseBuilder.Ask("Are we done here?");
```
Using Tell sets ShouldEndSession to true. Using Ask sets ShouldEndSession to false. 

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

## Get an audio request and determine the action

Once you know it is an AudioPlayerRequest, you have to determine which one (playback started, finished, stopped, failed) and respond accordingly.

```csharp
// do some audio response stuff
var audioRequest = input.Request as AudioPlayerRequest;

// these are events sent when the audio state has changed on the device
// determine what exactly happened
if (audioRequest.AudioRequestType == AudioRequestType.PlaybackNearlyFinished)
{
    // queue up another audio file
}
```

## Play an audio file

If your skill is registered as an audio player, you can send directives (instructions to play, enqueue, or stop an audio stream). 

```csharp
// create the speech response - you most likely will still have this
string audioUrl = "http://mydomain.com/myaudiofile.mp3";
string audioToken = "a token to describe the audio file"; 

var audioResponse = ResponseBuilder.AudioPlayerPlay(PlayBehavior.ReplaceAll, audioUrl, audioToken);

return audioResponse
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
