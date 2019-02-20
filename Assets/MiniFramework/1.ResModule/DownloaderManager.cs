using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    public class DownloaderManager : MonoSingleton<DownloaderManager>
    {
        //下载文件默认保存目录
        public  string DefaultSaveDir;

        private string downloadSpeed;
        private Dictionary<string, IDownloader> downloaderHandlers;

        private float countTime;
        private long lastTotalSaveLength;

        private DownloadState state;
        public override void OnSingletonInit(){
            DefaultSaveDir = Application.streamingAssetsPath;
            downloaderHandlers = new Dictionary<string, IDownloader>();
        }
        public string AddTask(string url)
        {
            return AddTask(url, DefaultSaveDir);
        }
        public string AddTask(string url,string saveDir)
        {
            IDownloader loader = new Downloader(url, saveDir);
            if (!downloaderHandlers.ContainsKey(loader.GetFileName()))
            {
                loader.DownloadSuccessed += DownloadSuccessedCallback;
                loader.DownloadFailed += DownloadFailedCallback;
                downloaderHandlers.Add(loader.GetFileName(), loader);
            }
            return loader.GetFileName();
        }
        public IDownloader GetTask(string name)
        {
            if (downloaderHandlers.ContainsKey(name))
            {
                return downloaderHandlers[name];
            }
            return null;
        }
        public int GetTaskCount()
        {
            return downloaderHandlers.Count;
        }
        public void RemoveTask(string name)
        {
            if (downloaderHandlers.ContainsKey(name))
            {
                downloaderHandlers[name].Close();
                downloaderHandlers.Remove(name);
            }
        }       
        public string GetDownloaderSpeed(float defaultUnitTime=0.5f)
        {
            countTime += Time.deltaTime;
            if (countTime >= defaultUnitTime)
            {
                long curTotalSaveLength = 0;
                foreach (var item in downloaderHandlers)
                {
                    curTotalSaveLength += item.Value.GetCurLength();
                }
                downloadSpeed = ByteUtil.AutoUnitConversion((curTotalSaveLength - lastTotalSaveLength) / defaultUnitTime) + "/S";
                lastTotalSaveLength = curTotalSaveLength;
                countTime = 0;
            }
            return downloadSpeed;
        }
        public float GetDownloadProcess()
        {
            float process = 0;
            foreach (var item in downloaderHandlers)
            {
                process += item.Value.GetDownloadProcess();
            }
            return process /= downloaderHandlers.Count;
        }
        public void Download()
        {
            lastTotalSaveLength = 0;
            foreach (var item in downloaderHandlers)
            {
                if (item.Value.GetDownloadSate() == DownloadState.None || item.Value.GetDownloadSate() == DownloadState.Failed)
                {
                    item.Value.Start();
                }
                lastTotalSaveLength += item.Value.GetCurLength();
            }
        }
        public void Pause()
        {
            foreach (var item in downloaderHandlers)
            {
                item.Value.Close();
            }
        }
        public void Pause(string name)
        {
            if (downloaderHandlers.ContainsKey(name))
            {
                downloaderHandlers[name].Close();
            }
        }
        void DownloadSuccessedCallback(string file)
        {
            Debug.Log(file + "：下载成功");
        }
        void DownloadFailedCallback(string file)
        {
            Debug.Log(file + "：下载失败");
        }
        void OnDestroy()
        {
            Pause();
        }
    }
}