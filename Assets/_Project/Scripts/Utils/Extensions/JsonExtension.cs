using Newtonsoft.Json;

namespace _Project.Scripts.Utils.Extensions
{
    public static class JsonExtension
    {
        public static T ToDeserialize<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
        public static string ToSerialize(this object obj) => JsonConvert.SerializeObject(obj);
    }
}