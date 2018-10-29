using UnityEngine;
using MiniFramework;
public class MsgSenderExample : MonoBehaviour,IMsgSender {

	// Use this for initialization
	void Start () {
        this.SendMsg(MsgDefine.Default, "hello world");
	}
}
