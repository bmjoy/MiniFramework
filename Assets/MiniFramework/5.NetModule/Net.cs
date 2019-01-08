using System;
using System.Net;
using System.Net.Sockets;

namespace MiniFramework
{
    public abstract class Net
    {
        public string IP;
        public int Port;
        public int MaxBufferSize;
        public int MaxConnections;
        public bool IsConnect { get; set; }
        public Action ConnectFailed;
        public Action ConnectSuccess;
        public DataPacker DataPacker;
        public void Init(string ip, int port, int maxBufferSize)
        {
            IP = ip;
            Port = port;
            MaxBufferSize = maxBufferSize;
            DataPacker = new DataPacker();
        }
        public void Init(string ip, int port, int maxBufferSize, int maxConnections)
        {
            Init(ip, port, maxBufferSize);
            MaxConnections = maxConnections;
        }
        public virtual void Launch(){}
        public virtual void Send(byte[] data, string ip = null){}
        public virtual void Send(PackHead head, byte[] data, string ip = null) { }
        public virtual void Close(){}
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