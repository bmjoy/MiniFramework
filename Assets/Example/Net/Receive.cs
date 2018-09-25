using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using System;
using UnityEngine.UI;

public class Receive : MonoBehaviour ,IMsgReceiver{
    public Text Text;
    bool getMsg;
    object msg;
	// Use this for initialization
	void Start () {
        this.RegisterMsg("1",ReceiveMsg);
	}
    private void Update()
    {
        if (getMsg)
        {
            Text.text = msg.ToString();
            GameObject text = Instantiate(Text.gameObject, Text.transform.parent);
            text.SetActive(true);
            getMsg = false;
        }
    }
    void ReceiveMsg(object[] data)
    {
        getMsg = true;
        msg = data[0];
    }
}
