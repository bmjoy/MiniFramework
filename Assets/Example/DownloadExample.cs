using UnityEngine;
using MiniFramework;
public class DownloadExample : MonoBehaviour
{
    string fileName="";
    private void Start()
    {
        // string url = "https://m10.music.126.net/20181116155517/c1d09d1ec123d97e17b5b84ddd12b2d6/ymusic/d043/68b7/0715/9f634e8962d185821121a406646ef2f0.mp3";
        // fileName = HttpDownloader.Instance.Download(url, Application.streamingAssetsPath);
        //string url = "http://music.163.com/api/search/get/web?csrf_token=hlpretag=&hlposttag=&s=" + "9420"+ "&type=1&offset=0&total=true&limit=10";
        string url2 = "http://music.163.com/api/song/detail/?" + "id=523114596&ids=%5B523114596%5D";
        this.HttpGet(url2, Callback);
    }

    void Callback(string txt)
    {
        Debug.Log(txt);
    }
    private void OnDestroy()
    {
        HttpDownloader.Instance.Dispose(fileName);
    }
}