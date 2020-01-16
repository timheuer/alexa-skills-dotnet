using Newtonsoft.Json.Linq;

namespace Alexa.NET.Request.Type
{
    public interface IDataDrivenRequestTypeConverter : IRequestTypeConverter
    {
        Request Convert(JObject data);
    }
}