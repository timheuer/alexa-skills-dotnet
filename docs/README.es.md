# Alexa Skills SDK para .NET

[![Build Status](https://dev.azure.com/timheuer/Alexa.NET/_apis/build/status/Alexa.NET-master?branchName=master)](https://dev.azure.com/timheuer/Alexa.NET/_build/latest?definitionId=2&branchName=master)
[![All Contributors](https://img.shields.io/badge/all_contributors-14-orange.svg?style=flat-square)](#contributors)

Alexa.NET es una librera auxiliar que permite trabajar con peticiones y respuestas para skills de Alexa en C#.  Si estas utilizando el servicio de AWS Lambda o tienes hospedado tu propio servicio en tu propio servidor, esta libreria tiene como objetivo simplemente hacer que trabajar con Alexa API sea mas natural para desarrolladores C#, usando un modelo de objetos fuertemente tipado.

Alexa.NET también sirve como base para un conjunto de extensiones adicionales para el desarrollo con alexa creado por [Steven Pears](https://github.com/stoiveyp):

* Management [GitHub](https://github.com/stoiveyp/Alexa.NET.Management) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Management)
* In-skill Pricing [GitHub](https://github.com/stoiveyp/Alexa.NET.InSkillPricing) / [NuGet](https://www.nuget.org/packages/Alexa.NET.InSkillPricing)
* Messaging [GitHub](https://github.com/stoiveyp/Alexa.NET.SkillMessaging) / [NuGet](https://www.nuget.org/packages/Alexa.NET.SkillMessaging)
* Gadgets [GitHub](https://github.com/stoiveyp/Alexa.NET.Gadgets) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Gadgets)
* Customer Profile API [GitHub](https://github.com/stoiveyp/Alexa.NET.CustomerProfile) / [NuGet](https://www.nuget.org/packages/Alexa.NET.CustomerProfile)
* Settings API [GitHub](https://github.com/stoiveyp/Alexa.NET.Settings) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Settings)
* APL Support [GitHub](https://github.com/stoiveyp/Alexa.NET.APL) / [NuGet](https://www.nuget.org/packages/Alexa.NET.APL)
* Reminders API [GitHub](https://github.com/stoiveyp/Alexa.NET.Reminders) / [NuGet](https://www.nuget.org/packages/Alexa.NET.Reminders)
* Proactive Events API [GitHub](https://github.com/stoiveyp/Alexa.NET.ProactiveEvents) / [NuGet](https://www.nuget.org/packages/Alexa.NET.ProactiveEvents)
* CanFulfillIntent Request Support [GitHub](https://github.com/stoiveyp/Alexa.NET.CanFulfillIntentRequest) / [NuGet](https://www.nuget.org/packages/Alexa.NET.CanFulfillIntentRequest)

# Configuración
Independiente de tu arquitectura, tu función para alexa deberá aceptar un SkillRequest y retornar un SkillResponse y la deserialización de la petición dentro del objecto SkillRequest denpenderá de tu framework.
```csharp
public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
{
    // La logica de tu función va aquí
    return new SkillResponse("OK");
}
```

# Tabla de contenido
- [Alexa Skills SDK para .NET](#Alexa-Skills-SDK-para-NET)
- [Configuración](#Configuraci%C3%B3n)
- [Tabla de contenido](#Tabla-de-contenido)
- [Tipos de peticiones](#Tipos-de-peticiones)
  - [Manipulando la petición AccountLinkSkillEventRequest](#Manipulando-la-petici%C3%B3n-AccountLinkSkillEventRequest)
  - [Manipulando la petición AudioPlayerRequest](#Manipulando-la-petici%C3%B3n-AudioPlayerRequest)
    - [Tipos de AudioRequestType](#Tipos-de-AudioRequestType)
  - [Manipulando la petición DisplayElementSelectedRequest](#Manipulando-la-petici%C3%B3n-DisplayElementSelectedRequest)
  - [Manipulando la petición IntentRequest](#Manipulando-la-petici%C3%B3n-IntentRequest)
    - [Intent](#Intent)
  - [Manipulando la petición LaunchRequest](#Manipulando-la-petici%C3%B3n-LaunchRequest)
  - [Manipulando la petición PermissionSkillEventRequest](#Manipulando-la-petici%C3%B3n-PermissionSkillEventRequest)
  - [Manipulando la petición PlaybackControllerRequest](#Manipulando-la-petici%C3%B3n-PlaybackControllerRequest)
  - [Manipulando la petición SessionEndedRequest](#Manipulando-la-petici%C3%B3n-SessionEndedRequest)
  - [Manipulando la petición SkillEventRequest](#Manipulando-la-petici%C3%B3n-SkillEventRequest)
  - [Manipulando la petición SystemExceptionRequest](#Manipulando-la-petici%C3%B3n-SystemExceptionRequest)
- [Respuestas](#Respuestas)
  - [Ask Vs Tell](#Ask-Vs-Tell)
  - [SSML Response](#SSML-Response)
  - [SSML Response With Card](#SSML-Response-With-Card)
  - [SSML Response With Reprompt](#SSML-Response-With-Reprompt)
  - [Play Audio File](#Play-Audio-File)
- [Variables de sesión](#Variables-de-sesi%C3%B3n)
  - [Respuesta con variables de sesión](#Respuesta-con-variables-de-sesi%C3%B3n)
  - [Recuperando variables de sesión de una solicitud](#Recuperando-variables-de-sesi%C3%B3n-de-una-solicitud)
- [Respuestas sin funciones auxiliares](#Respuestas-sin-funciones-auxiliares)
- [Respuestas progresivas](#Respuestas-progresivas)
  - [Contribuidores](#Contribuidores)


# Tipos de peticiones
Alexa enviará diferentes tipos de peticiones dependiendo de los eventos que deba responder. A continuación se encuentran todos los tipos de peticiones:

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

## Manipulando la petición AccountLinkSkillEventRequest
Esta petición es usada para enlazar Alexa con otra cuenta. La peticion vendrá con el token de acceso necesario para interactuar con el servicio conectado. Estos eventos solo se envian si han sido suscritos.

```csharp
var accountLinkReq = input.Request as AccountLinkSkillEventRequest;
var accessToken = accountLinkReq.AccessToken;
```

## Manipulando la petición AudioPlayerRequest
La petición de reproducción de audio se envía cuando un skill pretende reproducir audio o si un cambio en le estado de este ha ocurrido en el dispositivo.

```csharp
// hacer lago con la respuesta del audio
var audioRequest = input.Request as AudioPlayerRequest;

if (audioRequest.AudioRequestType == AudioRequestType.PlaybackNearlyFinished)
{
    // Poner en cola otro archivo de audio
}
```

### Tipos de AudioRequestType

Cada ```AudioPlayerRequest``` viene también con un tipo de petición que describe el cambio de estado:
- ```PlaybackStarted```
- ```PlaybackFinished```
- ```PlaybackStopped```
- ```PlaybackNearlyFinished```
- ```PlaybackFailed```

## Manipulando la petición DisplayElementSelectedRequest
La petición de mostar elemento seleccionado será enviada cuando un skill tiene GUI (Interfaz gráfica de usuario) y uno de los botones fue seleccionado por el usuario. Esta petición llega con un token que dirá que elemento de la interfaz fue seleccionado.

```csharp
var elemSelReq = input.Request as DisplayElementSelectedRequest;
var buttonID = elemSelReq.Token;
```

## Manipulando la petición IntentRequest
Este es problamente el tipo mas usado. IntentRequest también viene con un objeto ```Intent``` y un ```DialogState``` con valor ```STARTED```, ```IN_PROGRESS``` o ```COMPLETED```

### Intent
Cada intención (intent) esta definida por el nombre configurado en la consola de desarrollo de Alexa. Si tu intención (intent) incluye spacios (slots) serán incluidas en el objeto junto con el estado de confirmación.

```csharp
var intentRequest = input.Request as IntentRequest;

// Revisa el nombre que determina lo que se debe hacer
if (intentRequest.Intent.Name.Equals("MyIntentName"))
{
   if(intentRequest.DialogState.Equals("COMPLETED"))
   {
       // obtiene los espacios
       var firstValue = intentRequest.Intent.Slots["FirstSlot"].Value;
    }
}
```

## Manipulando la petición LaunchRequest
Este tipo de petición es enviado cuando tu skill es abierto sin una intención (intent) específica. Deberias recibir y procesar un ```IntentRequest```.

```csharp
if(input.Request is LaunchRequest)
{
   return ResponseBuilder.Ask("How can I help you today?");
}

```

## Manipulando la petición PermissionSkillEventRequest
Este evento es enviado cuando un cliente otorga o anula permisos.
Esta petición incluirá un objeto tipo ```SkillEventPermissions``` que incluye el cambio en los permisos. Estos eventos solamente se envian si han sido suscritos.

```csharp
var permissionReq = input.Request as PermissionSkillEventRequest;
var firstPermission = permissionReq.Body.AcceptedPermissions[0];
```

## Manipulando la petición PlaybackControllerRequest
Este evento es enviado para controlar la reproducción de fondo de un skill que contiene un reproductor de audio.

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

## Manipulando la petición SessionEndedRequest
Este evento es enviado si el usuario solicita salir, si su respuesta toma mucho tiempo o ha ocurrido un error en el dispositivo.

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

## Manipulando la petición SkillEventRequest
Este evento es enviado cuando el usuario habilita o desabilita el skill. Estos eventos solomente se envian si han sido suscritos.

## Manipulando la petición SystemExceptionRequest
Cuando un error ocurre, bien sea como resultado de un evento mal estructurado o de muchas peticiones, Alexa retornará un mensaje al cliente que incluye el código de la excepción y una descripción.

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

# Respuestas

## Ask Vs Tell
Hay 2 métodos auxiliares para formar una respuesta de voz con ```ResponseBuilder```:

```csharp
var finalResponse = ResponseBuilder.Tell("We are done here.");
var openEndedResponse = ResponseBuilder.Ask("Are we done here?");
```
Usando 'Tell' asignamos ```ShouldEndSession``` el valor ```true```. Usando 'Ask' asignamos ```ShouldEndSession``` el valor ```false```. Se debe usar la función apropiada dependiendo si queremos continuar el diálogo o no.

## SSML Response
SSML puede ser usada para personalizar la forma en que Alexa habla. A continuación vemos un ejemplo usando SSML con las funciones auxiliares.

```csharp
// build the speech response 
var speech = new SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.<break strength=\"x-strong\"/>I hope you have a good day.</speak>";

// create the response using the ResponseBuilder
var finalResponse = ResponseBuilder.Tell(speech);
return finalResponse;
```

## SSML Response With Card
En las respuestas también puedes incluir un 'Card', el cual representa elementos UI (Interfaz gráfica) para Alexa. ```ResponseBuilder``` solo construye 'cards' simples, que contienen un titulo y un texto plano.

```csharp
// crea la respuesta de habla - 'cards' todavia necesitan respuesta de voz
var speech = new SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// crea la respuesta con card
var finalResponse = ResponseBuilder.TellWithCard(speech, "Your Card Title", "Your card content text goes here, no HTML formatting honored");
return finalResponse;

```

## SSML Response With Reprompt
Si quieres un reprompt (doble aviso) para el usuario, usa la función auxiliar de Ask (Pregunta). Un reprompt puede ser útil si deseas continuar una conversación or si deseas recordar al usuario responder una pregunta.

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
Si tu skill esta registrado como reproductor de audio, puede enviar alguna de estas directivas (Instrucciones para reproducir, encolar or detener un audio).

```csharp
// crea una respuesta de habla
string audioUrl = "http://mydomain.com/myaudiofile.mp3";
string audioToken = "a token to describe the audio file"; 

var audioResponse = ResponseBuilder.AudioPlayerPlay(PlayBehavior.ReplaceAll, audioUrl, audioToken);

return audioResponse
```

# Variables de sesión
Las variables de sesión son variables que pueden ser guardadas dentro de una respuesta y serán enviadas de ida y de regreso mientras la sesión permanezca abierta.
## Respuesta con variables de sesión

```csharp
string speech = "The time is twelve twenty three.";
Session session = input.Session;

if(session.Attributes == null)
    session.Attributes = new Dictionary<string, object>();
session.Attributes["real_time"] = DateTime.Now;

return ResponseBuilder.Tell(speech, session);
```

## Recuperando variables de sesión de una solicitud

```csharp
Session session = input.Session;
DateTime lastTime = session.Attributes["real_time"] as DateTime;

return ResponseBuilder.Tell("The last day you asked was at " + lastTime.DayOfWeek.ToString());
```

# Respuestas sin funciones auxiliares
Si no quieres usar las funciones auxiliares Tell/Ask, Puedes construir la respuesta manualmente usando los objetos ```Response``` y ```IOutputSpeech```. Si quisieras incluir ```StandardCard``` o ```LinkAccountCard``` dentro de la respuesta, puedes hacerlo dentro del cuerpo de la repuesta:

```csharp
// crea la respuesta de habla
var speech = new SsmlOutputSpeech();
speech.Ssml = "<speak>Today is <say-as interpret-as=\"date\">????0922</say-as>.</speak>";

// crea el doble aviso (reprompt)
var repromptMessage = new PlainTextOutputSpeech();
repromptMessage.Text = "Would you like to know what tomorrow is?";

// crea el objecto del doble aviso (reprompt)
var repromptBody = new Reprompt();
repromptBody.OutputSpeech = repromptMessage;

// crea la respuesta
var responseBody = new ResponseBody();
responseBody.OutputSpeech = speech;
responseBody.ShouldEndSession = false; // esto desencadena el doble aviso
responseBody.Reprompt = repromptBody;
responseBody.Card = new SimpleCard {Title = "Test", Content = "Testing Alexa"};

var skillResponse = new SkillResponse();
skillResponse.Response = responseBody;
skillResponse.Version = "1.0";

return skillResponse;
```
# Respuestas progresivas
Tu skill puede enviar respuestas progresivas para mantener al usuario enganchado mientras este prepara la respuesta completa o respuesta final de la petición. A continuación un ejemplo de una respuesta progresiva:
```csharp
var progressiveResponse = new ProgressiveResponse(skillRequest);
progressiveResponse.SendSpeech("Please wait while I gather your data.");
```

## Contribuidores

Gracias a estas maravillosas personas ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table>
  <tr>
    <td align="center"><a href="http://www.imacode.ninja"><img src="https://avatars3.githubusercontent.com/u/147125?v=4" width="100px;" alt="Steven Pears"/><br /><sub><b>Steven Pears</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=stoiveyp" title="Code">💻</a> <a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=stoiveyp" title="Documentation">📖</a> <a href="#tutorial-stoiveyp" title="Tutorials">✅</a> <a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=stoiveyp" title="Tests">⚠️</a></td>
    <td align="center"><a href="http://vineetyadav.com"><img src="https://avatars1.githubusercontent.com/u/7949851?v=4" width="100px;" alt="yadavvineet"/><br /><sub><b>yadavvineet</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=yadavvineet" title="Code">💻</a></td>
    <td align="center"><a href="https://www.linkedin.com/in/soc-sieng-b45a9473/"><img src="https://avatars1.githubusercontent.com/u/2647062?v=4" width="100px;" alt="Soc Sieng"/><br /><sub><b>Soc Sieng</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=socsieng" title="Documentation">📖</a></td>
    <td align="center"><a href="http://www.stuartbuchanan.co.uk"><img src="https://avatars3.githubusercontent.com/u/16162689?v=4" width="100px;" alt="Stuart Buchanan"/><br /><sub><b>Stuart Buchanan</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=fuzzysb" title="Tests">⚠️</a></td>
    <td align="center"><a href="https://github.com/rdlaitila"><img src="https://avatars0.githubusercontent.com/u/1124388?v=4" width="100px;" alt="Regan Laitila"/><br /><sub><b>Regan Laitila</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=rdlaitila" title="Code">💻</a></td>
    <td align="center"><a href="https://github.com/IcanBENCHurCAT"><img src="https://avatars1.githubusercontent.com/u/4152550?v=4" width="100px;" alt="IcanBENCHurCAT"/><br /><sub><b>IcanBENCHurCAT</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=IcanBENCHurCAT" title="Documentation">📖</a></td>
    <td align="center"><a href="https://www.ydeho.com/"><img src="https://avatars3.githubusercontent.com/u/29730840?v=4" width="100px;" alt="VinceGusmini"/><br /><sub><b>VinceGusmini</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=VinceGusmini" title="Code">💻</a></td>
  </tr>
  <tr>
    <td align="center"><a href="http://www.yasoon.com"><img src="https://avatars2.githubusercontent.com/u/2111803?v=4" width="100px;" alt="Tobias Viehweger"/><br /><sub><b>Tobias Viehweger</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=tobiasviehweger" title="Code">💻</a></td>
    <td align="center"><a href="http://matthiasshapiro.com"><img src="https://avatars3.githubusercontent.com/u/235365?v=4" width="100px;" alt="Matthias Shapiro"/><br /><sub><b>Matthias Shapiro</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=matthiasxc" title="Code">💻</a> <a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=matthiasxc" title="Tests">⚠️</a></td>
    <td align="center"><a href="http://techpreacher.corti.com"><img src="https://avatars1.githubusercontent.com/u/841949?v=4" width="100px;" alt="Sascha Corti"/><br /><sub><b>Sascha Corti</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=TechPreacher" title="Code">💻</a></td>
    <td align="center"><a href="https://github.com/StenmannsAr"><img src="https://avatars3.githubusercontent.com/u/27204921?v=4" width="100px;" alt="StenmannsAr"/><br /><sub><b>StenmannsAr</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=StenmannsAr" title="Code">💻</a></td>
    <td align="center"><a href="https://adriangodong.com"><img src="https://avatars3.githubusercontent.com/u/1140137?v=4" width="100px;" alt="Adrian Godong"/><br /><sub><b>Adrian Godong</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=adriangodong" title="Code">💻</a></td>
    <td align="center"><a href="http://jad.codes"><img src="https://avatars0.githubusercontent.com/u/68933?v=4" width="100px;" alt="Jaddie"/><br /><sub><b>Jaddie</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/issues?q=author%3Ajaddie" title="Bug reports">🐛</a></td>
    <td align="center"><a href="https://github.com/evgeni-nabokov"><img src="https://avatars3.githubusercontent.com/u/3168823?v=4" width="100px;" alt="Evgeni Nabokov"/><br /><sub><b>Evgeni Nabokov</b></sub></a><br /><a href="https://github.com/timheuer/alexa-skills-dotnet/commits?author=evgeni-nabokov" title="Code">💻</a></td>
  </tr>
</table>

<!-- ALL-CONTRIBUTORS-LIST:END -->

Este proyecto sigue la especificación [all-contributors](https://github.com/all-contributors/all-contributors). Contribuciones de cualquier tipo son bienvenidas.
