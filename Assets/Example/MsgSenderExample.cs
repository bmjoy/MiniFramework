using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class MsgSenderExample : MonoBehaviour,IMsgSender {

	// Use this for initialization
	void Start () {
        this.SendMsg("xx", "hello world");
	}
}
