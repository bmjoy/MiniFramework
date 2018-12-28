//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;
//namespace MiniFramework
//{
//    public class SocketManager : MonoSingleton<SocketManager>
//    {
        
//        public SocketType Type;
//        public string IP;
//        public int Port;
//        public int MaxBufferSize;
//        public int MaxConnections;

//        public UDP UDP;
//        public TCPServer TCPServer;
//        public TCPClient TCPClient;
//        public override void OnSingletonInit()
//        {
//            MsgManager.Instance.RegisterMsg(this, "SocketManager", MsgCallback);
//            // Init();
//        }

//        private void Init()
//        {
//            switch (Type)
//            {
//                case SocketType.UDP:
//                    UDP = new UDP();
//                    UDP.Init();
//                    break;
//                case SocketType.TCPServer:
//                    TCPServer = new TCPServer();
//                    TCPServer.Init();
//                    break;
//                case SocketType.TCPClient:
//                    TCPClient = new TCPClient();
//                    TCPClient.Init();
//                    break;
//            }
//        }
//        void MsgCallback(params object[] data)
//        {
//            string msg = Encoding.UTF8.GetString((byte[])data[0]);
//            Debug.Log("接收消息：" + msg);
//        }
        
//        private void OnDestroy()
//        {
//            if (UDP != null)
//                UDP.Close();
//            if (TCPServer != null)
//                TCPServer.Close();
//            if (TCPClient != null)
//                TCPClient.Close();
//        }
//    }

//}
