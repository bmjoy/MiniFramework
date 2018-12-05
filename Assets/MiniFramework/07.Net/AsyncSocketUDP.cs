using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
namespace MiniFramework
{
    public class AsyncSocketUDP
    {
        private UdpClient client;
        private byte[] recvBuffer;
        private int maxBufferSize = 1024;
        public AsyncSocketUDP()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 7777);
                client = new UdpClient(ep);
                recvBuffer = new byte[maxBufferSize];
                client.EnableBroadcast = true;
                client.BeginReceive(ReceiveDataAsync, null);
                Debug.Log("本机：" + client.Client.LocalEndPoint);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return;
            }
        }
        private void ReceiveDataAsync(IAsyncResult ar)
        {
            IPEndPoint remote = null;
            recvBuffer = client.EndReceive(ar, ref remote);
            this.SendMsg("SocketManager", recvBuffer, remote);
            if (client != null)
            {
                client.BeginReceive(ReceiveDataAsync, null);
            }
        }
        public void Send(byte[] data, string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client.BeginSend(data, data.Length, ep, new AsyncCallback(SendCallback), null);
        }
        private void SendCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                try
                {
                    client.EndSend(ar);
                    Debug.Log("发送成功");
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        public void Stop()
        {
            if (client != null)
                client.Close();
        }
    }
}

