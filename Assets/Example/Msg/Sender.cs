using UnityEngine;
using MiniFramework;
public class Sender : MonoBehaviour, IMsgSender
{

    // Use this for initialization
    void Start()
    {
        this.SendMsg("1", 1);
    }
}