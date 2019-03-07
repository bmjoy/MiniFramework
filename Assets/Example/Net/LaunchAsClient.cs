using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using System.Text;
using System;

public class LaunchAsClient : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
		MsgManager.Instance.RegisterMsg(this,"1",Recv);
        SocketManager.Instance.Connect("192.168.0.101", 1122);
    }
	void Recv(params object[] data){
		byte[] bytes = (byte[])data[0];
		Debug.Log(Encoding.UTF8.GetString(bytes));
	}
    void Update()
    {
        if (SocketManager.Instance.TCPClient.IsConnect)
        {
            if (Time.frameCount % 120 == 0)
            {
                byte[] bytes = Encoding.UTF8.GetBytes("I am Client");
                PackHead head = new PackHead();
                head.MsgID = 1;
                head.TimeStamp = DateTime.Now.Second;
                head.BodyLength = bytes.Length;
				SocketManager.Instance.TCPClient.Send(head,bytes);
            }
        }
    }
}
