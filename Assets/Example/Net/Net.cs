using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniFramework;
using System;
using System.Text;

public class Net : MonoBehaviour,IMsgReceiver{
    public Text IPText;
    private List<string> msgs = new List<string>();
    public Text ReceText;
    public Toggle Client;
    public Toggle Server;
    public InputField SendText;
    public Button Send;
	// Use this for initialization
	void Start () {
        //this.RegisterMsg("ip", Callback);

        this.RegisterMsg("1", Callback);

        Client.onValueChanged.AddListener((s) => {
            if (s)
            {
                SocketManager.Instance.LaunchAsClient();
            }
            else
            {
                SocketManager.Instance.Client.Close();
            }
        });
        Server.onValueChanged.AddListener((s) => {
            if (s)
            {
                SocketManager.Instance.LaunchAsServer();
            }
            else
            {
                SocketManager.Instance.Server.Close();
            }
        });
        Send.onClick.AddListener(() =>
        {
            if (Client.isOn)
            {
                byte[] data = Encoding.UTF8.GetBytes(SendText.text);
                SocketManager.Instance.Client.Send(1, data);
            }
            else if(Server.isOn)
            {
                byte[] data = Encoding.UTF8.GetBytes(SendText.text);
                SocketManager.Instance.Server.Send(1, data);
            }
        });
    }

    private void Update()
    {
        if (msgs.Count > 0)
        {
            for (int i = msgs.Count-1; i >=0 ; i--)
            {
                ReceText.text = msgs[i];
                Instantiate(ReceText.gameObject,ReceText.transform.parent).SetActive(true);
                msgs.RemoveAt(i);
            }
            
        }
    }

    void Callback(object[] o)
    {
        byte[] data = (byte[])(o[0]);
        byte[] needData = new byte[data.Length - 4];
        Array.Copy(data, 4, needData, 0, needData.Length);
        string msg = Encoding.UTF8.GetString(needData);
        msgs.Add(msg);
    }
}
