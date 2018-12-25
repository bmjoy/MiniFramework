using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace MiniFramework
{
    public class Json:SerializeFactory
    {
        public override byte[] Serialize(object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return Encoding.UTF8.GetBytes(json);
        }
        public override T Deserialize<T>(byte[] data)
        {
            string json = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

