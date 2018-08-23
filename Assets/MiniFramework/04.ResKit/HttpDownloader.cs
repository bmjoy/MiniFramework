using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
namespace MiniFramework
{
    public enum DownState
    {
        None,
        Start,
        Stop,
        Completed
    }
    public class HttpDownloader
    {
        private Dictionary<int,DownloadTask> tasks = new Dictionary<int, DownloadTask>();
        public DownState GetStatus(int id)
        {
            return tasks[id].GetStatus();
        }
        public float GetProcess(int id)
        {
            return tasks[id].GetProcess();
        }
        public void Download(int id, MonoBehaviour mono, string url, string savePath, Action callBack = null)
        {       
            if (!tasks.ContainsKey(id))
            {
                DownloadTask task = new DownloadTask(mono, url, savePath, callBack);
                tasks.Add(id,task);
            }
        }     
    }
    public class DownloadTask

    {
        private DownState state;
        private string url;
        private string tempSavePath;
        private string savePath;
        private long curLength;
        private long fileLength;
        private Action CallBack;
        public DownState GetStatus()
        {
            return state;
        }
        public float GetProcess()
        {
            if (fileLength > 0)
            {
                return Mathf.Clamp((float)curLength / fileLength, 0, 1);
            }
            return 0;
        }
        public  DownloadTask(MonoBehaviour mono, string url, string savePath, Action callBack = null)
        {
            state = DownState.None;
            this.url = url;
            tempSavePath = savePath + ".temp";
            this.savePath = savePath;
            CallBack = callBack;
            mono.StartCoroutine(Task());
        }
        IEnumerator Task()
        {
            state = DownState.Start;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            FileStream fileStream;
            if (File.Exists(tempSavePath))
            {
                fileStream = File.OpenWrite(tempSavePath);
                curLength = fileStream.Length;
                fileStream.Seek(curLength, SeekOrigin.Current);
                request.AddRange((int)curLength);
            }
            else
            {
                fileStream = new FileStream(tempSavePath, FileMode.Create, FileAccess.Write);
                curLength = 0;
            }
            request.KeepAlive = false;
            WebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            fileLength = response.ContentLength + curLength;
            int lengthOnce;
            long bufferMaxLength = response.ContentLength;
            while (curLength < fileLength)
            {
                byte[] buffer = new byte[bufferMaxLength];
                if (stream.CanRead)
                {
                    lengthOnce = stream.Read(buffer, 0, buffer.Length);
                    curLength += lengthOnce;
                    fileStream.Write(buffer, 0, lengthOnce);
                }
                else
                {
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
            response.Close();
            stream.Close();
            fileStream.Close();
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            File.Move(tempSavePath, savePath);
            if (CallBack != null)
            {
                CallBack();
            }
            state = DownState.Completed;
        }
    }
}
