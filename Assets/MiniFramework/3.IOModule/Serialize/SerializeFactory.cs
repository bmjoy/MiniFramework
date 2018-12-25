namespace MiniFramework
{
    public abstract class SerializeFactory
    {
        public static Binary Binary { get => new Binary(); }
        public static Xml Xml { get => new Xml(); }
        public static Json Json { get => new Json(); }
        public static ProtoBuff ProtoBuff { get => new ProtoBuff(); }
        public abstract byte[] Serialize(object obj);
        public abstract T Deserialize<T>(byte[] data);
    }

}