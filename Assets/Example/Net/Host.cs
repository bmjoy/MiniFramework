using MiniFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Host : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((s) => {
            if (s)
            {
                SocketManager.Instance.LaunchAsServer();
            }
            else
            {
                SocketManager.Instance.Server.Close();
            }
        });
    }
}
