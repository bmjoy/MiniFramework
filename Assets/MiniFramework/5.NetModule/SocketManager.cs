using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>
    {
        public enum SocketType
        {
            UDP,
            TCP,
        }
        public SocketType Type;
        public bool IsServer;
        public string IP;
        public int Port;
        public int MaxBufferSize;
        public int MaxConnections;

        private SocketBase socket;
        private void Start()
        {
            MsgManager.Instance.RegisterMsg(this, "SocketManager", MsgCallback);
            Launch();
        }

        void Launch()
        {
            switch (Type)
            {
                case SocketType.UDP:
                    socket = new UDP(IP,Port,MaxBufferSize);
                    break;
                case SocketType.TCP:
                    if(IsServer)
                        socket = new TCPServer(Port,MaxBufferSize, MaxConnections);  
                    else
                        socket = new TCPClient(IP, Port, MaxBufferSize);
                    break;
            }
            socket.ConnectSuccess = ConnectSuccess;
            socket.ConnectFailed = ConnectFailed;

            socket.Launch();
        }
        public void Send(InputField inField)
        {
            socket.Send(Encoding.UTF8.GetBytes(inField.text));
        }
        void MsgCallback(params object[] data)
        {
            string msg = Encoding.UTF8.GetString((byte[])data[0]);
            Debug.Log("接收消息：" + msg);
        }

        void ConnectSuccess()
        {
            
        }
        void ConnectFailed()
        {
            socket.Launch();
        }
        private void OnDestroy()
        {
            socket.Close();
        }
    }

}
