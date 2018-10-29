using UnityEngine;
using MiniFramework;
public class MsgReceiverExample : MonoBehaviour,IMsgReceiver {

	// Use this for initialization
	void Start () {
        this.RegisterMsg(MsgDefine.Default, (param) => Debug.Log("我接收到了:"+param[0]));
	}
}
