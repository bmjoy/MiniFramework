using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace MiniFramework
{
    public class Binary : SerializeFactory
    {
        public override byte[] Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public override T Deserialize<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}