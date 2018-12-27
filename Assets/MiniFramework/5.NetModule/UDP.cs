using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class UDP : SocketBase
    {
        private string ip;
        private int port;
        private byte[] recvBuffer;
        private UdpClient udpClient;
        public UDP(string ip, int port, int bufferSize)
        {
            try
            {
                this.ip = ip;
                this.port = port;
                recvBuffer = new byte[bufferSize];
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                udpClient = new UdpClient(endPoint);
                udpClient.EnableBroadcast = true;
            }
            catch (Exception e)
            {

                Debug.Log(e);
            }
        }
        public override void Launch()
        {
            if (udpClient != null)
            {
                udpClient.BeginReceive(ReceiveResult, udpClient);
            }
        }
        private void ReceiveResult(IAsyncResult ar)
        {
            udpClient = (UdpClient)ar.AsyncState;
            IPEndPoint remote = null;
            recvBuffer = udpClient.EndReceive(ar, ref remote);
            MsgManager.Instance.SendMsg("SocketManager", recvBuffer);
            udpClient.BeginReceive(ReceiveResult, udpClient);
        }

        public override void Send(byte[] data)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            udpClient.BeginSend(data, data.Length, endPoint, SendResult, udpClient);
        }

        private void SendResult(IAsyncResult ar)
        {
            udpClient = (UdpClient)ar.AsyncState;
            udpClient.EndSend(ar);
        }

        public override void Close()
        {
            if (udpClient != null)
                udpClient.Close();
            Debug.Log("连接已断开");
        }
    }
}
