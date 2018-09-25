using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>,IMsgSender
    {
        public string ServerIP="127.0.0.1";
        public int ServerPort=8888;
        public Server Server;
        public Client Client;
        public void LaunchAsServer()
        {
            Server = new Server(ServerPort);
        }
        public void LaunchAsClient()
        {
            Client = new Client(ServerIP, ServerPort);
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
                    byte[] receBytes = ReadDataPackage(socket);
                    ByteBuffer buffer = new ByteBuffer(receBytes);
                    string data = buffer.ReadString();
                    Debug.Log(data);
                    this.SendMsg("1", data);
                }
                catch (Exception e)
                {
                    socket.Close();
                    Debug.Log(e);
                    break;
                }
            }
        }
        private byte[] ReadDataPackage(Socket socket)
        {
            int headLength = 4;
            byte[] receHead = new byte[headLength];
            int recedHeadLength = socket.Receive(receHead, headLength, 0);
            Debug.Log("接收数据头大小：" + recedHeadLength);
            if (recedHeadLength <= 0)
            {
                Debug.Log("数据包头小于等于0！");
                return null;
            }
            int BodyLength = BitConverter.ToInt32(receHead, 0);

            while (BodyLength > 0)
            {
                byte[] receBody = new byte[BodyLength];
                int recedBodyLength = socket.Receive(receBody, receBody.Length, 0);
                Debug.Log("接收数据主体大小：" + recedBodyLength);
                if (recedBodyLength > 0)
                {
                    return receBody;
                }
                break;
            }
            return null;
        }
        public string GetIPv4()
        {
            IPAddress[] ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            for (int i = 0; i < ips.Length; i++)
            {
                if(ips[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ips[i].ToString();
                }
            }
            return "";
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
