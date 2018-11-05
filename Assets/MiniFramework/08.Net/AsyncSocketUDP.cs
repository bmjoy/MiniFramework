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
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            client = new UdpClient(ep);
            recvBuffer = new byte[maxBufferSize];
            client.EnableBroadcast = true;
            client.BeginReceive(ReceiveDataAsync, null);
            Debug.Log("本机：" + client.Client.LocalEndPoint);
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="ar"></param>
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

        private void SplitPack(byte[] data, IPEndPoint ep)
        {
            if (data.Length > maxBufferSize)
            {
                int count = data.Length / maxBufferSize;
                for (int i = 0; i <= count; i++)
                {
                    byte[] buffer = new byte[maxBufferSize];
                    if (i == count)
                        Array.Copy(data, i * maxBufferSize, buffer, 0, data.Length - i * maxBufferSize);
                    else
                        Array.Copy(data, i * maxBufferSize, buffer, 0, maxBufferSize);
                    client.BeginSend(buffer, buffer.Length, ep, new AsyncCallback(SendCallback), null);
                }
            }
        }
    }
}

