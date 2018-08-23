using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class SocketExample : MonoBehaviour
{
    public List<string> IPList;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetIP());
        //EnumComputers();
       // GetIPThread();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetIPThread()
    {
        Thread thread = new Thread(new ThreadStart(EnumComputers));
        thread.Start();
    }
    void Send(object ip)
    {
        UnityEngine.Ping ping = new UnityEngine.Ping(ip.ToString());
        if (ping.isDone)
        {
            IPList.Add(ip.ToString());
        }
    }

    public IEnumerator GetIP()
    {
        string hostName = Dns.GetHostName();
        IPHostEntry entry = Dns.GetHostEntry(hostName);
        string hostIP = "";
        for (int i = 0; i < entry.AddressList.Length; i++)
        {
            if (entry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                hostIP = entry.AddressList[i].ToString();
                break;
            }
        }
        string ipDuan = hostIP.Remove(hostIP.LastIndexOf('.'));
        for (int i = 1; i <= 255; i++)
        {
            string pingIP = ipDuan + "." + i;
            UnityEngine.Ping ping = new UnityEngine.Ping(pingIP);
            yield return ping.isDone;
            Debug.Log(ping.time);
            if (ping.time>=0)
            {
                IPList.Add(ipDuan + "." + i);
            }
            ping.DestroyPing();
        }
        Debug.Log("结束");
    }


    private void EnumComputers()
    {
        string hostName = Dns.GetHostName();
        IPHostEntry entry = Dns.GetHostEntry(hostName);
        string hostIP = "";
        for (int i = 0; i < entry.AddressList.Length; i++)
        {
            if (entry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                hostIP = entry.AddressList[i].ToString();
                break;
            }
        }
        string ipDuan = hostIP.Remove(hostIP.LastIndexOf('.'));
        for (int i = 1; i <= 255; i++)
        {
            System.Net.NetworkInformation.Ping myPing = new System.Net.NetworkInformation.Ping();
            myPing.PingCompleted += new PingCompletedEventHandler(_myPing_PingCompleted);
            string pingIP = ipDuan + "." + i;
            myPing.SendAsync(IPAddress.Parse(pingIP), 1000, null);
        }
    }

    private void _myPing_PingCompleted(object sender, PingCompletedEventArgs e)
    {
        if (e.Reply.Status == IPStatus.Success)
        {
            IPList.Add(e.Reply.Address.ToString());
        }

    }
}
