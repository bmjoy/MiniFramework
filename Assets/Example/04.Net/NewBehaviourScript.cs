using UnityEngine;
using UnityEngine.UI;
using MiniFramework;
using System.Text;

public class NewBehaviourScript : MonoBehaviour {
    public Button Launch;
    public Button Send;
    public Button Close;

    public InputField IP;
    public InputField Msg;
	// Use this for initialization
	void Start () {
        Launch.onClick.AddListener(() =>
        {
            SocketManager.Instance.Net.Launch();
        });
        Send.onClick.AddListener(() => {
            byte[] data = Encoding.UTF8.GetBytes(Msg.text);
            SocketManager.Instance.Net.Send(data,IP.text);
        });
        Close.onClick.AddListener(() =>
        {
            SocketManager.Instance.Net.Close();
        });
	}
}
