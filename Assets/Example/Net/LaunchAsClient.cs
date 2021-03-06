﻿using System.Collections;
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
	void Recv(object data){
		byte[] bytes = (byte[])data;
		Debug.Log(Encoding.UTF8.GetString(bytes));
	}
    void Update()
    {
        if (SocketManager.Instance.Client.IsConnect)
        {
            if (Time.frameCount % 120 == 0)
            {
                byte[] bytes = Encoding.UTF8.GetBytes("I am Client");
                PackHead head = new PackHead();
                head.MsgID = 1;
                head.TimeStamp = DateTime.Now.Second;
                head.PackLength = (short)bytes.Length;
				SocketManager.Instance.Client.Send(head,bytes);
            }
        }
    }
}
