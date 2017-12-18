namespace Alexa.NET.Request.Type
{
    public class SkillEventRequestTypeConverter : IRequestTypeConverter
    {
        public bool CanConvert(string requestType)
        {
            return requestType == "AlexaSkillEvent.SkillPermissionAccepted" ||
                   requestType == "AlexaSkillEvent.SkillPermissionChanged" ||
                   requestType == "AlexaSkillEvent.SkillAccountLinked" ||
                   requestType.StartsWith("AlexaSkillEvent");
        }

        public Request Convert(string requestType)
        {
            if (requestType == "AlexaSkillEvent.SkillAccountLinked")
            {
                return new AccountLinkSkillEventRequest();
            }

            if (requestType == "AlexaSkillEvent.SkillPermissionAccepted" || requestType == "AlexaSkillEvent.SkillPermissionChanged")
            {
                return new PermissionSkillEventRequest();
            }

            return new SkillEventRequest();
        }
    }
}