using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class TCPClient : Net
    {
        private byte[] recvBuffer;
        private TcpClient tcpClient;
        public override void Launch()
        {
            if (IsConnect)
            {
                Debug.Log("客户端已连接");
                return;
            }
            recvBuffer = new byte[MaxBufferSize];
            tcpClient = new TcpClient();
            tcpClient.BeginConnect(IPAddress.Parse(IP), Port, ConnectResult, tcpClient);
        }
        private void ConnectResult(IAsyncResult ar)
        {
            tcpClient = (TcpClient)ar.AsyncState;
            if (!tcpClient.Connected)
            {
                if (ConnectFailed != null)
                    ConnectFailed();
                Debug.Log("连接服务器失败，请尝试重新连接!");
            }
            else
            {
                tcpClient.EndConnect(ar);
                NetworkStream stream = tcpClient.GetStream();
                stream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, tcpClient);
                IsConnect = true;
                if (ConnectSuccess != null)
                    ConnectSuccess();
                Debug.Log("客户端连接成功");

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
                DataPacker.UnPack(recvBytes);
                stream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, tcpClient);
            }
        }

        public override void Send(byte[] data, string ip = null)
        {
            if (IsConnect)
            {
                tcpClient.GetStream().BeginWrite(data, 0, data.Length, SendResult, tcpClient);
            }
        }
        public override void Send(PackHead head,byte[] data,string ip = null)
        {
            byte[] sendData = DataPacker.Packer(head, data);
            if (IsConnect)
            {
                tcpClient.GetStream().BeginWrite(sendData, 0, sendData.Length, SendResult, tcpClient);
            }
        }
        private void SendResult(IAsyncResult ar)
        {
            tcpClient = (TcpClient)ar.AsyncState;
            NetworkStream stream = tcpClient.GetStream();
            stream.EndWrite(ar);
        }
        public override void Close()
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

