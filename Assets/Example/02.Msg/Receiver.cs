using UnityEngine;
using MiniFramework;
public class Receiver : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        MsgManager.Instance.RegisterMsg(this, "Test", Receive);
    }

    void Receive(object[] msg)
    {
        Debug.Log(msg[0].ToString());
    }
}
