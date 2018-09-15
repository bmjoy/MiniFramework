using MiniFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SocketManager.Instance.serverIP = "127.0.0.1";
        SocketManager.Instance.serverPort = 6000;
        SocketManager.Instance.Connect();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
