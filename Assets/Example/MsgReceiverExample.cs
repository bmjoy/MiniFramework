using UnityEngine;
using MiniFramework;
public class MsgReceiverExample : MonoBehaviour {
	// Use this for initialization
	void Start () {
        this.RegisterMsg("MsgReceiver1",(param) => Debug.Log(this.name+"接收到了:"+param[0]));
        this.RegisterMsg("MsgReceiver2",(param) => Debug.Log(this.name+"再次接收到了:" + param[0]));
    }
}
