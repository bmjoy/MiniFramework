using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>, IMsgReceiver,IMsgSender
    {
        private AsyncSocketUDP aSocketUDP;
        private Queue<byte[]> MsgQueue;
        // Use this for initialization
        void Start()
        {
            aSocketUDP = new AsyncSocketUDP();
            MsgQueue = new Queue<byte[]>();
            this.RegisterMsg(MsgDefine.Net, Receiver);
        }
        public void Send(byte[] data, string ip, int port)
        {
            aSocketUDP.Send(data, ip, port);
            Debug.Log("发送数据大小：" + data.LongLength);
        }
        void Receiver(params object[] o)
        {
            byte[] data = (byte[])o[0];
            IPEndPoint remote = (IPEndPoint)o[1];
            MsgQueue.Enqueue(data);
            Debug.Log(Encoding.UTF8.GetString(data) + "\n来自：" + remote.ToString());
        }
        private void Update()
        {
            if (MsgQueue.Count > 0)
            {
                this.SendMsg(MsgDefine.Default,MsgQueue.Dequeue());
            }
        }
    }
}