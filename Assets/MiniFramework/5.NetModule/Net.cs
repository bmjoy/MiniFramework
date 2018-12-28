using System;
using System.Net;
using System.Net.Sockets;

namespace MiniFramework
{
    public abstract class Net
    {
        public enum SocketType
        {
            UDP,
            TCPServer,
            TCPClient,
        }
        public SocketType Type;
        public string IP;
        public int Port;
        public int MaxBufferSize;
        public int MaxConnections;
        public Net() { }
        public Net(string ip, int port, int maxBufferSize)
        {
            IP = ip;
            Port = port;
            MaxBufferSize = maxBufferSize;
        }
        public Net(string ip, int port, int maxBufferSize, int maxConnections)
        {
            IP = ip;
            Port = port;
            MaxBufferSize = maxBufferSize;
            MaxConnections = maxBufferSize;
        }
        public TCPClient TCPClient { get; set; }

        public TCPServer TCPServer { get; set; }

        public UDP UDP { get; set; }
        public abstract void Close();
        public Net Launch()
        {
            switch (Type)
            {
                case SocketType.TCPClient:
                    TCPClient = new TCPClient();
                    break;

            }
            return this;
        }
        public string GetLocalIP()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            for (int i = 0; i < ipEntry.AddressList.Length; i++)
            {
                if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipEntry.AddressList[i].ToString();
                }
            }
            return "";
        }
    }
}