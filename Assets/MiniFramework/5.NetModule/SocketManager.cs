namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>
    {
        public enum SocketType
        {
            TCPClient,
            TCPServer,
            UDP,
        }
        public SocketType Type;
        public string IP;
        public int Port;
        public int MaxBufferSize;
        public int MaxConnections;

        private Net Net;
        protected override void OnSingletonInit()
        {
            Init();
        }
        private void Init()
        {
            switch (Type)
            {
                case SocketType.UDP:
                    Net = new UDP();
                    break;
                case SocketType.TCPServer:
                    Net = new TCPServer();
                    break;
                case SocketType.TCPClient:
                    Net = new TCPClient();
                    break;
            }
            Net.Init(IP, Port, MaxBufferSize, MaxConnections);
        }
        public void Launch()
        {
            Net.Launch();
        }

        public void Send(int msgID, byte[] data, string ip = null)
        {
            PackHead head = new PackHead();
            head.MsgID = msgID;
            head.BodyLength = data.Length;
            Net.Send(head, data, ip);
        }
        public void Close()
        {
            Net.Close();
            Net.OtherBytes = new byte[0];
        }
        private void OnDestroy()
        {
            if (Net != null)
                Net.Close();
        }
    }

}
