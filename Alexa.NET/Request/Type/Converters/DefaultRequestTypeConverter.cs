namespace Alexa.NET.Request.Type
{
    public class DefaultRequestTypeConverter : IRequestTypeConverter
    {
        public bool CanConvert(string requestType)
        {
            return requestType == "IntentRequest" || requestType == "CanFulfillIntentRequest" || requestType == "LaunchRequest" || requestType == "SessionEndedRequest" || requestType == "System.ExceptionEncountered";
        }

        public Request Convert(string requestType)
        {
            switch (requestType)
            {
                case "IntentRequest":
                    return new IntentRequest();
                case "CanFulfillIntentRequest":
                    return new IntentRequest();
                case "LaunchRequest":
                    return new LaunchRequest();
                case "SessionEndedRequest":
                    return new SessionEndedRequest();
                case "System.ExceptionEncountered":
                    return new SystemExceptionRequest();
            }
            return null;
        }
    }
}
