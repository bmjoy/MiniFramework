namespace MiniFramework
{
    public abstract class SerializeFactory
    {
        public static Binary Binary { get { return new Binary(); } }
        public static Xml Xml { get { return new Xml(); } }
        public static Json Json { get { return new Json(); } }
        public static ProtoBuff ProtoBuff { get { return new ProtoBuff(); } }
        public abstract byte[] Serialize(object obj);
        public abstract T Deserialize<T>(byte[] data);
    }
}