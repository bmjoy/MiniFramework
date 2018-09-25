using UnityEngine;
using UnityEngine.UI;
using MiniFramework;
public class Refresh : MonoBehaviour,IMsgReceiver {
    public Text Text;
    bool getMsg;
    object Msg;
    // Use this for initialization
    void Start () {
        
        this.RegisterMsg("2", Receive);
        GetComponent<Button>().onClick.AddListener(() => {
            //UdpBroadcast.Instance.Receive();
        });
        UdpBroadcast.Instance.Receive();
    }

    private void Update()
    {
        if (getMsg)
        {
            Text.text = Msg.ToString();
            GameObject text = Instantiate(Text.gameObject, Text.transform.parent);
            text.SetActive(true);
            getMsg = false;
        }
    }
    void Receive(object[] param)
    {
        getMsg = true;
        Msg = param[0];
    }
}
