using MiniFramework;
using UnityEngine;
using UnityEngine.UI;

public class DownExample : MonoBehaviour
{
    // Use this for initialization
    public Slider Slider;
    void Start()
    {
        string url = "http://sinacloud.net/zackzhang/blogsfile/SetupFactory9.0.3.0Trial.zip";
        HttpDownloader.Instance.Download(1,url, Application.streamingAssetsPath + "/1.zip", OnDownComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if (HttpDownloader.Instance.GetStatus(1) == DownState.Start)
        {
            Debug.Log(HttpDownloader.Instance.GetProcess(1));
            Slider.value = HttpDownloader.Instance.GetProcess(1);
        }
    }

    void OnDownComplete()
    {
        Debug.Log("下载完成");
    }
}