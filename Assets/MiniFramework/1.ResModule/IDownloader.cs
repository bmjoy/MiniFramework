using System;

public interface IDownloader {
    /// <summary>
    /// 获取下载进度
    /// </summary>
    /// <returns></returns>
    float GetDownloadProcess();
    /// <summary>
    /// 下载成功监听事件
    /// </summary>
    event Action<string> DownloadSuccessed;
    /// <summary>
    /// 下载失败监听事件
    /// </summary>
    event Action<string> DownloadFailed;
    /// <summary>
    /// 开始下载
    /// </summary>
    void Start();
    /// <summary>
    /// 关闭/暂停下载
    /// </summary>
    void Close();
}
