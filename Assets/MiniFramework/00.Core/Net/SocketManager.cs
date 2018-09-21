using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>,IMsgSender
    {
        public string serverIP="127.0.0.1";
        public int serverPort=8888;
        public Server Server;
        public Client Client;
        private byte[] tempReceiveBuff = new byte[4096];
        public void LaunchAsServer()
        {
            Server = new Server(serverIP, serverPort);
        }
        public void LaunchAsClient()
        {
            Client = new Client(serverIP, serverPort);
        }
        
        public void Receive(object o)
        {
            Socket socket = (Socket)o;
            while (true)
            {
                if (!socket.Connected)
                {
                    Debug.LogError("Socket连接中断！");
                    socket.Close();
                    break;
                }
                try
                {
                    int receiveLength = socket.Receive(tempReceiveBuff);
                    if (receiveLength > 0)
                    {
                        ByteBuffer buff = new ByteBuffer(tempReceiveBuff);
                        string data = buff.ReadString();
                        Debug.Log(data);
                        this.SendMsg("1", data);
                    }
                }
                catch (Exception e)
                {
                    socket.Close();
                    Debug.Log(e);
                    break;
                }
            }
        }
        private void OnDestroy()
        {
            if (Server != null)
            {
                Server.Close();
            }
            if (Client != null)
            {
                Client.Close();
            }           
        }
    }
}
