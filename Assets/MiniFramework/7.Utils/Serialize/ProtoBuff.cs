using System.IO;
namespace MiniFramework
{
    public static class ProtoBuff
    {
        public static byte[] Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static T Deserialize<T>(byte[] bytes)
        {
            return ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(bytes));
        }
    }
}

