using System.IO;
using System.Xml.Serialization;

namespace MiniFramework
{
    public static class Xml
    {
        public static byte[] Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                xml.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static T Deserialize<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                object obj = xml.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}

