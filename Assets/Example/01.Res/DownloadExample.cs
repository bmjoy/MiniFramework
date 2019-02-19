using UnityEngine;
using MiniFramework;
public class DownloadExample : MonoBehaviour {
    string url = "https://netstorage.unity3d.com/unity/76b3e37670a4/UnityDownloadAssistant-2018.3.5f1.exe";
    Downloader downloader;
	// Use this for initialization
	void Start () {
        downloader = new Downloader();
        downloader.Start(url, Application.streamingAssetsPath);
        downloader.DownloadSuccessed += DownloadSuccess;
        downloader.DownloadFailed += DownloadFailed;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(downloader.GetDownloadProcess());
        Debug.Log(downloader.GetDownloadSpeed());
    }
    void DownloadFailed(string name)
    {
        Debug.Log(name + "下载失败");
    }
    void DownloadSuccess(string name)
    {
        Debug.Log(name + "下载成功");
    }

    private void OnDestroy()
    {
        downloader.Close();
    }
}
