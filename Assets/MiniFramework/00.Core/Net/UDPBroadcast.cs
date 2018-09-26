using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
namespace MiniFramework
{
    public class UdpBroadcast : MonoSingleton<UdpBroadcast>, IMsgSender
    {
        public int ServerPort=4444;
        private UdpClient udpRece;
        private UdpClient udpSend;
        private Thread receThread;
        public void Broadcast()
        {        
            udpSend = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            IPEndPoint targetPoint = new IPEndPoint(IPAddress.Broadcast, ServerPort);
            string ip = SocketManager.Instance.GetIPv4();
            byte[] data = Encoding.UTF8.GetBytes(ip);
            udpSend.Send(data, data.Length, targetPoint);
        }

        public void Receive()
        {
            if (receThread == null)
            {
                receThread = new Thread(rece);
                receThread.IsBackground = true;
                receThread.Start();
            }       
        }

        void rece()
        {
            udpRece = new UdpClient(new IPEndPoint(IPAddress.Any, ServerPort));
            IPEndPoint recePoint = new IPEndPoint(IPAddress.Any,0);
            while (true)
            {
                try
                {
                    Byte[] receiveBytes = udpRece.Receive(ref recePoint);                 
                    string msg = Encoding.UTF8.GetString(receiveBytes);
                    Debug.Log("Udp广播信息：" + msg + "\nIP端口：" + recePoint);
                    SocketManager.Instance.HostIP = msg;
                    this.SendMsg("ip", msg);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    break;
                }
            }

        }      
        public void CloseReceive()
        {
            if (udpRece != null)
            {
                udpRece.Close();
            }
            if (receThread != null)
            {
                receThread.Interrupt();
                receThread.Abort();
            }           
        }
        private void OnDestroy()
        {
            CloseReceive();
        }
    }
}

