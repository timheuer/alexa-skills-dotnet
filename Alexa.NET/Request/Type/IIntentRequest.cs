namespace Alexa.NET.Request.Type
{
    public interface IIntentRequest : IRequest
    {
        Intent Intent { get; set; }
    }
}