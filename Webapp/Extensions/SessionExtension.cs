using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Webapp.Extensions
{
    public static class SessionExtension
    {
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default : JsonSerializer.Deserialize<T>(data);
        }

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            var data = JsonSerializer.Serialize(value);
            session.SetString(key, data);
        }
    }
}
