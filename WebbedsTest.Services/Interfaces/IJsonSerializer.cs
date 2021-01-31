namespace WebbedsTest.Services.Interfaces
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string json);
    }
}
