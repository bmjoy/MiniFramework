using System;
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
    public class HttpDownloader:Singleton<HttpDownloader>
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
        private HttpDownloader() { }
        public void Download(int id,string url, string savePath, Action callBack = null)
        {       
            if (!tasks.ContainsKey(id))
            {
                DownloadTask task = new DownloadTask(url, savePath, callBack);
                tasks.Add(id,task);
            }
        }
    }
    public class DownloadTask
    {
        private DownState state;
        private string url;//下载地址
        private string tempSavePath;//临时文件保存路径
        private string savePath;//下载文件保存路径
        private long curLength;//临时文件长度
        private long fileLength;//下载文件总长度
        private Action CallBack;

        private FileStream fileStream;
        private HttpWebResponse response;
        private Stream responseStream;
        private byte[] buffer = new byte[1024];
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
        public  DownloadTask(string url, string savePath, Action callBack = null)
        {
            state = DownState.None;
            this.url = url;
            tempSavePath = savePath + ".temp";
            this.savePath = savePath;
            CallBack = callBack;
            DownTask();
        }

        void DownTask()
        {
            state = DownState.Start;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
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
            request.BeginGetResponse(new AsyncCallback(RespCallBack), request);
        }
        void RespCallBack(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            response = (HttpWebResponse)request.EndGetResponse(result);
            responseStream = response.GetResponseStream();
            fileLength = response.ContentLength + curLength;
            responseStream.BeginRead(buffer, 0, buffer.Length,new AsyncCallback(ReadCallBack), responseStream);
        }

        void ReadCallBack(IAsyncResult result)
        {
            responseStream = (Stream)result.AsyncState;
            int read = responseStream.EndRead(result);
            curLength += read;
            if (read > 0)
            {
                fileStream.Write(buffer,0,read);
                responseStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallBack), responseStream);
            }
            else
            {
                fileStream.Close();
                responseStream.Close();
                response.Close();
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
}
