using Newtonsoft.Json;

public static class SessionExtensions
{
    public static void SetObject<T>(this ISession session, string key, T value)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto // Позволяет сериализовать сложные объекты
        };
        session.SetString(key, JsonConvert.SerializeObject(value, settings));
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        if (value == null)
        {
            return default;
        }

        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto // Обеспечивает корректную десериализацию
        };
        return JsonConvert.DeserializeObject<T>(value, settings);
    }
    
}