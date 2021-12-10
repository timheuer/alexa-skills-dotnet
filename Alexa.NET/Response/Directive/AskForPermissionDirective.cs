namespace Alexa.NET.Response.Directive
{
    public class AskForPermissionDirective : ConnectionSendRequest<AskForPermissionPayload>
    {
        public AskForPermissionDirective()
        {
            
        }

        public AskForPermissionDirective(string permissionScope)
        {
            this.Payload = new AskForPermissionPayload(permissionScope);
        }
    }
}