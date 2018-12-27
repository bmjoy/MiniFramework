using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class TCPClient : SocketBase
    {
        private string ip;
        private int port;
        private byte[] recvBuffer;
        private TcpClient tcpClient;
        public TCPClient(string ip, int port, int bufferSize)
        {
            try
            {
                this.ip = ip;
                this.port = port;
                recvBuffer = new byte[bufferSize];
                tcpClient = new TcpClient();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        public override void Launch()
        {
            if (tcpClient != null && !tcpClient.Connected)
            {
                tcpClient.BeginConnect(IPAddress.Parse(ip), port, ConnectResult, tcpClient);
            }
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
                    tcpClient.Close();
                    Debug.Log("服务器已经关闭");
                    return;
                }
                byte[] realBytes = new byte[recvLength];
                Array.Copy(recvBuffer, 0, realBytes, 0, recvLength);
                stream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, tcpClient);
                MsgManager.Instance.SendMsg("SocketManager", realBytes);
            }
        }

        public override void Send(byte[] data)
        {
            if (tcpClient.Connected)
            {
                tcpClient.GetStream().BeginWrite(data, 0, data.Length, SendResult, tcpClient);
            }
        }
        private void SendResult(IAsyncResult ar)
        {
            tcpClient = (TcpClient)ar.AsyncState;
            NetworkStream stream = tcpClient.GetStream();
            stream.EndWrite(ar);
            Debug.Log("发送成功");
        }
        public override void Close()
        {
            if (tcpClient != null)
                tcpClient.Close();
            Debug.Log("连接已断开");
        }
    }
}

