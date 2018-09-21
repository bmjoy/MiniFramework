using MiniFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((s) => {
            if (s)
            {
                SocketManager.Instance.LaunchAsClient();
            }
            else
            {
                SocketManager.Instance.Client.Close();
            }
        });
	}
	
	
}
