using System;
using System.Net;
using System.Net.Sockets;

namespace MiniFramework
{
    public abstract class Net : DataPacker
    {
        public string IP;
        public int Port;
        public int MaxBufferSize;
        public int MaxConnections;
        public bool IsConnect { get; set; }
        public Action ConnectFailed;
        public Action ConnectSuccess;
        public void Init(string ip, int port, int maxBufferSize)
        {
            IP = ip;
            Port = port;
            MaxBufferSize = maxBufferSize;
        }
        public void Init(string ip, int port, int maxBufferSize, int maxConnections)
        {
            Init(ip, port, maxBufferSize);
            MaxConnections = maxConnections;
        }
        public abstract void Launch();
        public abstract void Send(byte[] data, string ip = null);
        public virtual void Send(PackHead head, byte[] data, string ip = null) { }
        public abstract void Close();
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