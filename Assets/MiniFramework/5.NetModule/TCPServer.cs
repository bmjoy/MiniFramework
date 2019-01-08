using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MiniFramework
{
    public class TCPServer : Net
    {
        private byte[] recvBuffer;
        private List<TcpClient> remoteClients;
        private TcpListener tcpListener;
        public override void Launch()
        {
            if (IsConnect)
            {
                Debug.Log("服务器已启动");
                return;
            }
            recvBuffer = new byte[MaxBufferSize];
            remoteClients = new List<TcpClient>();
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, Port);
                tcpListener.Start(MaxConnections);
                tcpListener.BeginAcceptTcpClient(AcceptResult, tcpListener);
                IsConnect = true;
                Debug.Log("服务端启动成功:" + tcpListener.LocalEndpoint);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        private void AcceptResult(IAsyncResult ar)
        {
            tcpListener = (TcpListener)ar.AsyncState;
            TcpClient remoteClient = tcpListener.EndAcceptTcpClient(ar);
            remoteClients.Add(remoteClient);
            NetworkStream networkStream = remoteClient.GetStream();
            networkStream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, remoteClient);
            tcpListener.BeginAcceptTcpClient(AcceptResult, tcpListener);
            Debug.Log("远程客户端：" + remoteClient.Client.RemoteEndPoint + "接入成功");
        }

        private void ReadResult(IAsyncResult ar)
        {
            TcpClient tcpClient = (TcpClient)ar.AsyncState;
            if (tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();
                int recvLength = stream.EndRead(ar);
                if (recvLength <= 0)
                {
                    Debug.Log("远程客户端：" + tcpClient.Client.RemoteEndPoint + "已经断开");
                    remoteClients.Remove(tcpClient);
                    tcpClient.Close();
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
            if (!IsConnect)
            {
                return;
            }
            for (int i = 0; i < remoteClients.Count; i++)
            {
                TcpClient client = remoteClients[i];
                if (client.Connected)
                    client.GetStream().BeginWrite(data, 0, data.Length, SendResult, client);
            }
        }
        public override void Send(PackHead head, byte[] data, string ip = null)
        {
            if (!IsConnect)
            {
                return;
            }
            byte[] sendData = DataPacker.Packer(head, data);
            for (int i = 0; i < remoteClients.Count; i++)
            {
                TcpClient client = remoteClients[i];
                if (client.Connected)
                    client.GetStream().BeginWrite(sendData, 0, sendData.Length, SendResult, client);
            }
        }
        private void SendResult(IAsyncResult ar)
        {
            TcpClient tcpClient = (TcpClient)ar.AsyncState;
            NetworkStream stream = tcpClient.GetStream();
            stream.EndWrite(ar);
        }
        public override void Close()
        {
            if (remoteClients != null)
            {
                foreach (var item in remoteClients)
                {
                    if (item.Connected)
                    {
                        item.Close();
                    }
                }
                remoteClients.Clear();
            }
            if (tcpListener != null)
            {
                tcpListener.Stop();
                IsConnect = false;
            }
            Debug.Log("服务器已关闭");
        }
    }
}