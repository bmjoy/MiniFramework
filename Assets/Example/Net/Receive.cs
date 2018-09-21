using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using System;
using UnityEngine.UI;

public class Receive : MonoBehaviour ,IMsgReceiver{
    public Text Text;
	// Use this for initialization
	void Start () {
        this.RegisterMsg("1",ReceiveMsg);
	}

    void ReceiveMsg(object[] data)
    {
        Text.text = data[0].ToString();
        GameObject text = Instantiate(Text.gameObject, Text.transform);
        text.SetActive(true);
    }
}
