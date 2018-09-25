using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using UnityEngine.UI;
public class Send : MonoBehaviour
{
    public InputField Text;
    public Button BtSend;
    private void Start()
    {
        BtSend.onClick.AddListener(() =>
        {
            if (SocketManager.Instance.Server != null)
            {
                SocketManager.Instance.Server.Send(Text.text);
                SocketManager.Instance.Server.Send("1");
            }
            if (SocketManager.Instance.Client != null)
            {
                SocketManager.Instance.Client.Send(Text.text);
            }
        });
    }
}
