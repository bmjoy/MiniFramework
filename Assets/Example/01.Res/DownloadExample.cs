using UnityEngine;
using MiniFramework;
public class DownloadExample : MonoBehaviour
{
    public string[] Urls;
    public float Process;
    public string Speed;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Urls.Length; i++)
        {
            IDownloader downloader = DownloaderManager.Instance.AddTask(Urls[i]);
            downloader.DownloadSuccessed += DownloadSuccess;
            downloader.DownloadFailed += DownloadFailed;
        }
        DownloaderManager.Instance.StartDownload();
    }
    // Update is called once per frame
    void Update()
    { 
        Speed = DownloaderManager.Instance.GetDownloaderSpeed();
        Process = DownloaderManager.Instance.GetDownloadProcess();
    }
    void DownloadFailed(string name)
    {
        Debug.Log(name + ":下载失败");
    }
    void DownloadSuccess(string name)
    {
        Debug.Log(name + ":下载成功");
    }
}
