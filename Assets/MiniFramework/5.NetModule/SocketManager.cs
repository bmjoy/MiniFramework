using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using UnityEngine;
namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>
    {
        public int Timeout;
        public Client Client;
        public Server Server;
        public UDP UDP;
        public override void OnSingletonInit()
        {
            MsgManager.Instance.RegisterMsg(this,MsgConfig.Net.ConnectSuccess.ToString(),ConnectSuccess);
            MsgManager.Instance.RegisterMsg(this,MsgConfig.Net.ConnectFailed.ToString(),ConnectFailed);
            MsgManager.Instance.RegisterMsg(this,MsgConfig.Net.ConnectAbort.ToString(),ConnectAbort);
        }
        public void Connect(string ip, int port)
        {
            Client = new Client();
            Client.Connect(ip, port);
        }
        public void LaunchAsServer(int port, int maxConnections)
        {
            Server = new Server();
            Server.Launch(port, maxConnections);
        }
        public void LaunchAsHost(int port)
        {
            UDP = new UDP();
            UDP.Launch(port);
        }
        void ConnectSuccess(object data){

        }
        void ConnectFailed(object data){

        }
        void ConnectAbort(object data){

        }
        IEnumerator CheckTimeout(int timeout){
            yield return new WaitForSeconds(timeout);
            if(!Client.IsConnect){
                Close();
            }
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
            if (Client != null)
            {
                Client.Close();
            }
            if (Server != null)
            {
                Server.Close();
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
