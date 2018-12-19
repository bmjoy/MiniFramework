using UnityEngine;
using MiniFramework;
public class Sender : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MsgSender.SendMsg("Test", "Hello");
    }
}
