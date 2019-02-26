using System.IO;
namespace MiniFramework
{
    public class ProtoBuff:SerializeUtil
    {
        public override byte[] Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public override T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new System.ArgumentNullException("bytes");
            }
            return ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(bytes));
        }
    }
}

