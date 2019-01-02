using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class UDP:Net
    {
        private byte[] recvBuffer;
        private UdpClient udpClient;
        public override void Launch()
        {
            if (IsConnect)
            {
                Debug.Log("UDP已启动!");
                return;
            }
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
                udpClient = new UdpClient(endPoint);
                udpClient.EnableBroadcast = true;
                udpClient.BeginReceive(ReceiveResult, udpClient);
                IsConnect = true;
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
            UnPack(recvBuffer);
            udpClient.BeginReceive(ReceiveResult, udpClient);
        }
        public override void Send(byte[] data,string ip)
        {
            if (IsConnect)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), Port);
                udpClient.BeginSend(data, data.Length, endPoint, SendResult, udpClient);
            }
        }
        public override void Send(PackHead head, byte[] data, string ip = null)
        {
            byte[] sendData = Packer(head, data);
            if (IsConnect)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), Port);
                udpClient.BeginSend(sendData, sendData.Length, endPoint, SendResult, udpClient);
            }
        }
        private void SendResult(IAsyncResult ar)
        {
            udpClient = (UdpClient)ar.AsyncState;
            udpClient.EndSend(ar);
        }

        public override void Close()
        {
            if (udpClient != null)
            {
                udpClient.Close();
                IsConnect = false;
            }
            Debug.Log("连接已断开");
        }
    }
}