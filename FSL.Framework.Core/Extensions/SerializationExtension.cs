using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FSL.Framework.Core.Extensions
{
    public static class SerializationExtension
    {
        public static T FromJson<T>(
            this string json)
        {
            if (json == null || json.Length == 0)
                return default;

            return JsonConvert.DeserializeObject<T>(
                json, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static string ToJson<T>(
            this T obj)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(obj,
                 new JsonSerializerSettings()
                 {
                     NullValueHandling = NullValueHandling.Ignore
                 });
        }

        public static T FromXml<T>(
            this string xml)
        {
            if (string.IsNullOrEmpty(xml)) return default;

            T result;
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            using (StringReader str = new StringReader(xml))
            {
                result = (T)xmlSer.Deserialize(str);
            }

            return result;
        }

        public static string ToXml<T>(
            this T obj)
        {
            if (obj == null) return null;

            string result = "";
            XmlSerializer xmlSer = new XmlSerializer(obj.GetType());
            using (MemoryStream m = new MemoryStream())
            {
                xmlSer.Serialize(m, obj);
                result = Encoding.UTF8.GetString(m.GetBuffer()).Replace("\0", "");
            }

            return result;
        }
    }
}
