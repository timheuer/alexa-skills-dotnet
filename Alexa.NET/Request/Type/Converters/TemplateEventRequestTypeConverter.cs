namespace Alexa.NET.Request.Type
{
    public class TemplateEventRequestTypeConverter : IRequestTypeConverter
    {
        public bool CanConvert(string requestType)
        {
            return requestType == "Display.ElementSelected";
        }

        public Request Convert(string requestType)
        {
            return new DisplayElementSelectedRequest();
        }
    }
}