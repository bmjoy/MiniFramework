using System;
namespace MiniFramework
{
    public interface IDownloader
    {
        /// <summary>
        /// 获取下载文件名
        /// </summary>
        /// <returns></returns>
        string GetFileName();
        /// <summary>
        /// 获取当前下载大小
        /// </summary>
        /// <returns></returns>
        long GetCurLength();
        /// <summary>
        /// 获取下载进度
        /// </summary>
        /// <returns></returns>
        float GetDownloadProcess();
        /// <summary>
        /// 获取下载状态
        /// </summary>
        /// <returns></returns>
        DownloadState GetDownloadSate();
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

    public enum DownloadState
    {
        None,
        Downloading,
        Completed,
        Failed
    }
}

