using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class TCPClient
    {
        public bool IsConnect;
        public int MaxBufferSize = 1024;
        public Action ConnectFailed;
        public Action ConnectSuccess;

        private byte[] recvBuffer;
        private TcpClient tcpClient;
        private DataPacker dataPacker;
        public void Connect(string ip,int port)
        {
            if (IsConnect)
            {
                Debug.Log("客户端已连接");
                return;
            }
            recvBuffer = new byte[MaxBufferSize];
            dataPacker = new DataPacker();
            tcpClient = new TcpClient();
            tcpClient.BeginConnect(IPAddress.Parse(ip), port, ConnectResult, tcpClient);
        }
        private void ConnectResult(IAsyncResult ar)
        {
            tcpClient = (TcpClient)ar.AsyncState;
            if (!tcpClient.Connected)
            {  
                Debug.Log("连接服务器失败，请尝试重新连接!");
                if (ConnectFailed != null)
                    ConnectFailed();
            }
            else
            {
                tcpClient.EndConnect(ar);
                NetworkStream stream = tcpClient.GetStream();
                stream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, tcpClient);
                IsConnect = true;
                Debug.Log("客户端连接成功");
                if (ConnectSuccess != null)
                    ConnectSuccess();
            }
        }
        private void ReadResult(IAsyncResult ar)
        {
            tcpClient = (TcpClient)ar.AsyncState;
            if (tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();
                int recvLength = stream.EndRead(ar);
                if (recvLength <= 0)
                {
                    Debug.Log("服务器已经关闭");
                    Close();
                    return;
                }
                byte[] recvBytes = new byte[recvLength];
                Array.Copy(recvBuffer, 0, recvBytes, 0, recvLength);
                dataPacker.UnPack(recvBytes);
                stream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, tcpClient);
            }
        }
        public void Send(PackHead head,byte[] data)
        {
            byte[] sendData = dataPacker.Packer(head, data);
            Send(sendData);
        }
        public void Send(byte[] data)
        {
            if (IsConnect)
            {
                tcpClient.GetStream().BeginWrite(data, 0, data.Length, SendResult, tcpClient);
            }
        }
        private void SendResult(IAsyncResult ar)
        {
            tcpClient = (TcpClient)ar.AsyncState;
            NetworkStream stream = tcpClient.GetStream();
            stream.EndWrite(ar);
        }
        public void Close()
        {
            if (tcpClient != null)
            {
                tcpClient.Close();
                IsConnect = false;
            }
            Debug.Log("连接已断开");
        }
    }
}

