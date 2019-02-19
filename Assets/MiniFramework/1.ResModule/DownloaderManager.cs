using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    public class DownloaderManager : MonoSingleton<DownloaderManager>
    {
        //下载文件默认保存目录
        public string DefaultSaveDir;

        private string downloadSpeed;
        private Dictionary<string, Downloader> downloaderHandlers = new Dictionary<string, Downloader>();

        private float countTime;
        public override void OnSingletonInit()
        {
            DefaultSaveDir = Application.streamingAssetsPath;
        }

        public void AddTask(string url)
        {
            Downloader loader = new Downloader(url, DefaultSaveDir);
            if (!downloaderHandlers.ContainsKey(loader.FileName))
            {
                loader.DownloadSuccessed += DownloadSuccessedCallback;
                loader.DownloadFailed += DownloadFailedCallback;
                downloaderHandlers.Add(loader.FileName, loader);

                loader.Start();
            }
        }
        private void Update()
        {
            if (downloaderHandlers.Count > 0)
            {
                countTime += Time.deltaTime;
                if (countTime >= 1)
                {
                    long curTotalSaveLength = 0;
                    foreach (var item in downloaderHandlers)
                    {
                        curTotalSaveLength += item.Value.CurLength;
                    }
                    downloadSpeed = ByteUtil.AutoUnitConversion(curTotalSaveLength - lastTotalSaveLength) + "/S";
                    countTime = 0;
                }
            }
        }
        public string GetDownloaderSpeed()
        {
            StartCoroutine(downloaderSpeedCounter());
            return downloadSpeed;
        }
        void DownloadSuccessedCallback(string file)
        {
            downloaderHandlers.Remove(file);
        }
        void DownloadFailedCallback(string file)
        {
            downloaderHandlers.Remove(file);
        }
        IEnumerator downloaderSpeedCounter()
        {
            long lastTotalSaveLength = 0;
            foreach (var item in downloaderHandlers)
            {
                lastTotalSaveLength += item.Value.CurLength;
            }
            yield return new WaitForSeconds(1f);
            long curTotalSaveLength = 0;
            foreach (var item in downloaderHandlers)
            {
                curTotalSaveLength += item.Value.CurLength;
            }
            downloadSpeed = ByteUtil.AutoUnitConversion(curTotalSaveLength - lastTotalSaveLength) + "/S";
        }
    }
}