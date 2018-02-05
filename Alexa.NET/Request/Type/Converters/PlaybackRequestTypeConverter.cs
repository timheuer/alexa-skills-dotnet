namespace Alexa.NET.Request.Type
{
    public class PlaybackRequestTypeConverter : IRequestTypeConverter
    {
        public bool CanConvert(string requestType)
        {
            return requestType.StartsWith("PlaybackController");
        }

        public Request Convert(string requestType)
        {
            return new PlaybackControllerRequest();
        }
    }
}