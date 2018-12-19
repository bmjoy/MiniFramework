using System.IO;
namespace MiniFramework
{
    public class ProtoBuff
    {
        public static byte[] Serialize<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new System.ArgumentNullException("bytes");
            }
            return ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(bytes));
        }
    }
}

