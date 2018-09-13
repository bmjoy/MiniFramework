using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
namespace MiniFramework
{
    public class SocketManager
    {
        private string serverIP;
        private int serverPort;
        private Action connectCallback;
        private Action connectFailedCallback;
        private Socket socket;

        private bool isStopReceive = false;
        public void Connect()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(serverIP);
            IPEndPoint endPoint = new IPEndPoint(address, serverPort);
            IAsyncResult result = socket.BeginConnect(endPoint, ConnectCallback, socket);
            bool success = result.AsyncWaitHandle.WaitOne(5000, true);
            if (!success)
            {
                //超时
                Closed();
            }
            else
            {
                isStopReceive = false;
                Thread thread = new Thread(new ThreadStart(Receive));
                thread.IsBackground = true;
                thread.Start();
            }
        }
        public void Closed()
        {
            isStopReceive = true;
            if (socket != null && socket.Connected)
            {
                socket.Close();
            }
            socket = null;
        }
        void ConnectCallback(IAsyncResult ar)
        {
            if (socket.Connected)
            {
                Debug.Log("连接成功");
            }
            else
            {
                Debug.Log("连接失败");
            }
        }

        private void Send()
        {

        }
        private void Receive()
        {
            while (!isStopReceive)
            {
                if (!socket.Connected)
                {
                    socket.Close();
                    break;
                }
                try
                {
                    byte[] bytes = new byte[4096];
                    int i = socket.Receive(bytes);
                    if (i <= 0)
                    {
                        socket.Close();
                        break;
                    }
                }
                catch (Exception e)
                {
                    socket.Close();
                    break;
                }

            }
        }
    }

}
