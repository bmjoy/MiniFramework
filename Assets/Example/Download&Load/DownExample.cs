using MiniFramework;
using UnityEngine;
using UnityEngine.UI;

public class DownExample : MonoBehaviour
{
    // Use this for initialization
    public Slider Slider_0;
    public Slider Slider_1;
    string fileName_0;
    string fileName_1;
    void Start()
    {
        string url_0 = "http://sinacloud.net/zackzhang/blogsfile/SetupFactory9.0.3.0Trial.zip";
        string url_1 = "https://dl.tvcdn.de/download/TeamViewer_Setup.exe";
        fileName_0 = HttpDownloader.Instance.Download(url_0, Application.streamingAssetsPath, OnDownComplete);
        fileName_1 = HttpDownloader.Instance.Download(url_1, Application.streamingAssetsPath, OnDownComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if (HttpDownloader.Instance.GetStatus(fileName_0) == DownState.Start)
        {
            //Debug.Log(HttpDownloader.Instance.GetProcess(fileName_0));
            Slider_0.value = HttpDownloader.Instance.GetProcess(fileName_0);
        }
        if (HttpDownloader.Instance.GetStatus(fileName_1) == DownState.Start)
        {
           // Debug.Log(HttpDownloader.Instance.GetProcess(fileName_1));
            Slider_1.value = HttpDownloader.Instance.GetProcess(fileName_1);
        }
    }

    void OnDownComplete()
    {
        Debug.Log("下载完成");
    }
}