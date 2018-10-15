using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class MsgReceiverExample : MonoBehaviour,IMsgReceiver {

	// Use this for initialization
	void Start () {
        this.RegisterMsg("xx", (param) => Debug.Log("我接收到了:"+param[0]));
	}
}
