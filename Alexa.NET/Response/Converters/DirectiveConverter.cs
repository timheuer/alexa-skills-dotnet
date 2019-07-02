using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response.Converters
{
    public class DirectiveConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public static Dictionary<string, Func<IDirective>> TypeFactories = new Dictionary<string, Func<IDirective>>
        {
            { "AudioPlayer.Play", () => new AudioPlayerPlayDirective() },
            { "AudioPlayer.ClearQueue", () => new ClearQueueDirective() },
            { "Dialog.ConfirmIntent", () => new DialogConfirmIntent() },
            { "Dialog.ConfirmSlot", () => new DialogConfirmSlot() },
            { "Dialog.Delegate", () => new DialogDelegate() },
            { "Dialog.ElicitSlot", () => new DialogElicitSlot() },
            { "Display.RenderTemplate", () => new DisplayRenderTemplateDirective() },
            { "Hint", () => new HintDirective() },
            { "AudioPlayer.Stop", () => new StopDirective() },
            { "VideoApp.Launch", () => new VideoAppDirective() },
            { "Connections.StartDirective", () => new StartConnectionDirective() }
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var typeKey = jsonObject["type"] ?? jsonObject["Type"];
            var typeValue = typeKey.Value<string>();
            var hasFactory = TypeFactories.ContainsKey(typeValue);

            if (!hasFactory)
                throw new Exception(
                    $"unable to deserialize response. " +
                    $"unrecognized directive type '{typeValue}'"
                );

            var directive = TypeFactories[typeValue]();

            serializer.Populate(jsonObject.CreateReader(), directive);

            return directive;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IDirective);
        }
    }
}