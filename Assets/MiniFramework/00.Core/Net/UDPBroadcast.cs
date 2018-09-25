using System;
using System.Net;
using System.Net.Sockets;
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
            ByteBuffer data = new ByteBuffer();
           
            data.WriteString(SocketManager.Instance.GetIPv4());
            byte[] bytes = data.ToBytes();
            udpSend.Send(bytes, bytes.Length, targetPoint);
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
                    ByteBuffer buffer = new ByteBuffer(receiveBytes);
                    string msg = buffer.ReadString();
                    Debug.Log("Udp广播信息：" + msg + "\nIP端口：" + recePoint);
                    this.SendMsg("2", msg);
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

