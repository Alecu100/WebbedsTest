using Newtonsoft.Json;
using WebbedsTest.Services.Interfaces;

namespace WebbedsTest.Services
{
    public class JsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
