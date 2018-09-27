using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace MiniFramework
{
    public class Server
    {
        public Socket Socket;
        public Dictionary<string, Socket> ClientSocketDict = new Dictionary<string, Socket>();
        public Server(int port)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, SocketManager.Instance.HostPort);
            Socket.Bind(endPoint);
            Socket.Listen(SocketManager.Instance.MaxListens);
            Debug.Log("服务器启动成功!");
            Thread thread = new Thread(ListenCallback);
            thread.IsBackground = true;
            thread.Start();
        }

        void ListenCallback()
        {
            while (true)
            {
                try
                {
                    Socket clientSocket = Socket.Accept();
                    string EndPoint = clientSocket.RemoteEndPoint.ToString();
                    Debug.Log("客户端:" + EndPoint + "连接成功:");
                    if (ClientSocketDict.ContainsKey(EndPoint))
                    {
                        ClientSocketDict.Remove(EndPoint);
                    }
                    ClientSocketDict.Add(EndPoint, clientSocket);
                    Thread thread = new Thread(SocketManager.Instance.Receive);
                    thread.IsBackground = true;
                    thread.Start(clientSocket);
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    break;
                }
            }
        }

        public void Send(int command, byte[] data)
        {
            foreach (var item in ClientSocketDict)
            {
                if (item.Value.Connected)
                {
                    byte[] package = ByteBuffer.BuildDataPackage(command, data);
                    item.Value.Send(package);
                    Debug.Log("发送数据大小：" + package.Length);
                }
            }
        }
        public void Close()
        {
            if (Socket!=null)
            {
                foreach (var item in ClientSocketDict)
                {
                    item.Value.Close();
                }
                Debug.Log("已断开所有客户端连接!");
                ClientSocketDict.Clear();
                Socket.Close();
                Debug.Log("已关闭服务器！");
            }
        }
    }
}

