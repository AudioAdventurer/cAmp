using Newtonsoft.Json;

namespace cAmp.Libraries.Common.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize<T>(T obj, bool formatted = true)
        {
            var format = Formatting.Indented;
            if (!formatted)
            {
                format = Formatting.None;
            }

            return JsonConvert.SerializeObject(obj, format);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
