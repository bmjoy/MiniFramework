using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class UDP
    {
        protected int Port;
        private byte[] recvBuffer;
        private UdpClient udpClient;
        public void Init()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
                udpClient = new UdpClient(endPoint);
                udpClient.EnableBroadcast = true;
                udpClient.BeginReceive(ReceiveResult, udpClient);
                Debug.Log("UDP初始化成功");
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        private void ReceiveResult(IAsyncResult ar)
        {
            udpClient = (UdpClient)ar.AsyncState;
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, Port);
            recvBuffer = udpClient.EndReceive(ar, ref remote);
            MsgManager.Instance.SendMsg("SocketManager", recvBuffer);
            udpClient.BeginReceive(ReceiveResult, udpClient);
        }
        public void Send(byte[] data,string ip)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), Port);
            udpClient.BeginSend(data, data.Length, endPoint, SendResult, udpClient);
        }
        private void SendResult(IAsyncResult ar)
        {
            udpClient = (UdpClient)ar.AsyncState;
            udpClient.EndSend(ar);
        }

        public void Close()
        {
            if (udpClient != null)
            {
                udpClient.Close();
                udpClient = null;
            }
            Debug.Log("连接已断开");
        }
    }
}