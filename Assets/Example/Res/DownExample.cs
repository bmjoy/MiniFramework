using MiniFramework;
using UnityEngine;
using UnityEngine.UI;

public class DownExample : MonoBehaviour
{
    // Use this for initialization
    public Slider Slider;
    HttpDownloader http;
    void Start()
    {
        string url = "http://sinacloud.net/zackzhang/blogsfile/SetupFactory9.0.3.0Trial.zip";
        http = new HttpDownloader();
        http.Download(1,this, url, Application.streamingAssetsPath + "/1.zip", OnDownComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if (http.GetStatus(1) == DownState.Start)
        {
            Debug.Log(http.GetProcess(1));
            Slider.value = http.GetProcess(1);
        }
    }

    void OnDownComplete()
    {
        Debug.Log("下载完成");
    }
}