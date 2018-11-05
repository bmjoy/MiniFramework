using UnityEngine;
using MiniFramework;
public class MsgSenderExample : MonoBehaviour
{ 
    // Use this for initialization
    void OnEnable()
    {
        this.SendMsg("MsgReceiver1", "hello world");
        this.SendMsg("MsgReceiver2", "hello world2");
    }
}
