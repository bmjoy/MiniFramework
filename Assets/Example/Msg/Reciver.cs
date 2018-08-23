using UnityEngine;
using MiniFramework;
public class Reciver : MonoBehaviour, IMsgReceiver
{

    // Use this for initialization
    void Start()
    {
        this.RegisterMsg("1", (res) => { Debug.Log("我接收到了:" + (int)res[0]); });
    }
}