using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class UDP
    {
        public bool IsActive;
        private byte[] recvBuffer;
        private UdpClient udpClient;
        private DataPacker dataPacker;
        public void Launch(int port)
        {
            if (IsActive)
            {
                Debug.Log("UDP已启动!");
                return;
            }
            dataPacker = new DataPacker();
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                udpClient = new UdpClient(endPoint);
                udpClient.EnableBroadcast = true;
                udpClient.BeginReceive(ReceiveResult, udpClient);
                IsActive = true;
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
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            recvBuffer = udpClient.EndReceive(ar, ref remote);
            dataPacker.UnPack(recvBuffer);
            udpClient.BeginReceive(ReceiveResult, udpClient);
        }
        public void Send(byte[] data, string ip, int port)
        {
            if (IsActive)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                udpClient.BeginSend(data, data.Length, endPoint, SendResult, udpClient);
            }
        }
        public void Send(PackHead head, byte[] data, string ip, int port)
        {
            byte[] sendData = dataPacker.Packer(head, data);
            Send(sendData, ip, port);
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
                IsActive = false;
            }
            Debug.Log("连接已断开");
        }
    }
}