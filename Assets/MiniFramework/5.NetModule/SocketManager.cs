using System.Text;
using UnityEngine;
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

        public Net Net;
        public override void OnSingletonInit()
        {
            MsgManager.Instance.RegisterMsg(this, "SocketManager", MsgCallback);
        }
        private void Start()
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
            Net.Init(IP,Port,MaxBufferSize,MaxConnections);
            Net.ReceiveMsgHandler += (data) => MsgManager.Instance.SendMsg("SocketManager", data);
        }
        void MsgCallback(params object[] data)
        {
            string msg = Encoding.UTF8.GetString((byte[])data[0]);
            Debug.Log("接收消息：" + msg);
        }
        private void OnDestroy()
        {
            Net.Close();
        }
    }

}
