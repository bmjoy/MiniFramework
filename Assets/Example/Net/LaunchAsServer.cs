﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using System.Text;
using System;

public class LaunchAsServer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MsgManager.Instance.RegisterMsg(this,"1",Recv);
		SocketManager.Instance.LaunchAsServer(1122,10);
	}
	void Recv(object data){
		byte[] bytes = (byte[])data;
		Debug.Log(Encoding.UTF8.GetString(bytes));
	}
	// Update is called once per frame
	void Update () {
		if(Time.frameCount%60==0){
			byte[] bytes = Encoding.UTF8.GetBytes("I am Server");
			PackHead head= new PackHead();
			head.MsgID = 1;
			head.TimeStamp = DateTime.Now.Second;
			head.PackLength = (short)bytes.Length;			
			SocketManager.Instance.Server.Send(head,bytes);
		}
	}
}
