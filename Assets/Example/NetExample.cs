using UnityEngine;
using MiniFramework;
using System.Text;
using UnityEngine.UI;
using System;
public class NetExample : MonoBehaviour {

    public Text Text;
    public InputField InputText;
    public Button Send;

    public InputField remoIP;
    public InputField remoPort;
    private Action<string> receCallback;
	// Use this for initialization
	void Start () {
        Send.onClick.AddListener(() => {
            byte[] data = Encoding.UTF8.GetBytes(InputText.text);
            SocketManager.Instance.Send(data, remoIP.text, int.Parse(remoPort.text));
        });

        //this.RegisterMsg(Rece);
	}
    void Rece(object[] o) {
        Text.text = Encoding.UTF8.GetString((byte[])o[0]);
        Instantiate(Text.gameObject,Text.transform.parent).SetActive(true);
    }
}
