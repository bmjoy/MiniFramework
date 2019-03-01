﻿using UnityEngine;
using UnityEngine.UI;
using MiniFramework;
using System.Text;
using ProtoBuf;

public class TcpExample : MonoBehaviour {
    public Button Launch;
    public Button Send;
    public Button Close;

    public InputField IP;
    public InputField Msg;
    // Use this for initialization
    private void Awake()
    {
        MsgManager.Instance.RegisterMsg(this, "100", (s) => {
            MsgExample msg = SerializeUtil.ProtoBuff.Deserialize<MsgExample>((byte[])s[0]);
            Debug.Log("age:" + msg.age + "name:" + msg.name + "sex:" + msg.sex);
        });
    }
    void Start () {
        Launch.onClick.AddListener(() =>
        {
            SocketManager.Instance.Launch();
        });
        Send.onClick.AddListener(() => {
            MsgExample msg = new MsgExample();
            msg.age = 12;
            msg.name = Msg.text;
            msg.sex = 1;
            byte[] sendData = SerializeUtil.ProtoBuff.Serialize(msg);
            SocketManager.Instance.Send(100,sendData, IP.text);
            SocketManager.Instance.Send(100,sendData, IP.text);
            SocketManager.Instance.Send(100,sendData, IP.text);
        });
        Close.onClick.AddListener(() =>
        {
            SocketManager.Instance.Close();
        });
	}
}
[ProtoContract]
public class MsgExample
{
    [ProtoMember(1)]
    public int age;
    [ProtoMember(2)]
    public string name;
    [ProtoMember(3)]
    public short sex;
}