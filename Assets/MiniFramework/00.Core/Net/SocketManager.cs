using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>, IMsgSender
    {
        public string HostIP = "127.0.0.1";
        public int HostPort = 8888;
        public int MaxListens = 12;
        public Server Server;
        public Client Client;
        public void LaunchAsServer()
        {
            Server = new Server(HostPort);
        }
        public void LaunchAsClient()
        {
            Client = new Client(HostIP, HostPort);
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
                    ReadDataPackage(socket);
                }
                catch (Exception e)
                {
                    socket.Close();
                    Debug.Log(e);
                    break;
                }
            }
        }

        /// <summary>
        /// 分包
        /// </summary>
        /// <param name="receLength"></param>
        /// <param name="data"></param>
        private void ReadDataPackage(Socket socket)
        {
            byte[] head = new byte[4];
            int receLength = socket.Receive(head, head.Length, 0);
            if (receLength < 0)
            {               
                return;
            }
            int bodyLength = BitConverter.ToInt32(head, 0);
            byte[] body = new byte[bodyLength];
            receLength = socket.Receive(body, body.Length, 0);
            if (receLength <0)
            {
                return;
            }
            int command = BitConverter.ToInt32(body, 0);
            this.SendMsg(command.ToString(), body);
            Debug.Log("接收数据 消息头：" + head.Length + "消息主体：" + body.Length);
        }


        public string GetIPv4()
        {
            IPAddress[] ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
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
                Server = null;
            }
            if (Client != null)
            {
                Client.Close();
                Client = null;
            }
        }
    }
}
