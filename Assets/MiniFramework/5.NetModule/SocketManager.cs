using System;
using System.Net;
using System.Net.Sockets;

namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>
    {
        public TCPClient TCPClient;
        public TCPServer TCPServer;
        public UDP UDP;
        public Action ConnectFailed;
        public Action ConnectSuccess;
        public override void OnSingletonInit()
        {

        }
        public void Connect(string ip, int port)
        {
            TCPClient = new TCPClient();
            TCPClient.ConnectSuccess = ConnectSuccess;
            TCPClient.ConnectFailed = ConnectFailed;
            TCPClient.Connect(ip, port);
        }
        public void LaunchAsServer(int port, int maxConnections)
        {
            TCPServer = new TCPServer();
            TCPServer.Launch(port, maxConnections);
        }
        public void LaunchAsHost(int port)
        {
            UDP = new UDP();
            UDP.Launch(port);
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
        public void Close()
        {
            if (TCPClient != null)
            {
                TCPClient.Close();
            }
            if (TCPServer != null)
            {
                TCPServer.Close();
            }
            if (UDP != null)
            {
                UDP.Close();
            }
        }
        private void OnDestroy()
        {
            Close();
        }
    }
}
