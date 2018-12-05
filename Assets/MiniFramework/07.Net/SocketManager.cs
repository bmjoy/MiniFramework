using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
namespace MiniFramework
{
    public class SocketManager : MonoSingleton<SocketManager>
    {
        private AsyncSocketUDP aSocketUDP;
        // Use this for initialization
        void Start()
        {
            aSocketUDP = new AsyncSocketUDP();
            this.RegisterMsg("SocketManager", Receiver);
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
            Debug.Log(Encoding.UTF8.GetString(data) + "\n来自：" + remote.ToString());
        }
        private void OnDestroy()
        {
            aSocketUDP.Stop();
        }
    }
}