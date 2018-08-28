using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
namespace MiniFramework
{
    public enum DownState
    {
        None,
        Start,
        Stop,
        Completed
    }
    public class HttpDownloader : Singleton<HttpDownloader>
    {
        private Dictionary<string, DownloadTask> tasks = new Dictionary<string, DownloadTask>();
        public DownState GetStatus(string name)
        {
            if (tasks.ContainsKey(name))
                return tasks[name].GetStatus();
            return DownState.None;
        }
        public float GetProcess(string name)
        {
            if (tasks.ContainsKey(name))
                return tasks[name].GetProcess();
            return 0;
        }
        public void RemoveTask(string name)
        {
            if (tasks.ContainsKey(name))
                tasks.Remove(name);
        }
        private HttpDownloader() { }
        public string Download(string url, string saveDir, Action callBack = null)
        {
            string fileName = url.Substring(url.LastIndexOf('/') + 1);
            if (!tasks.ContainsKey(fileName))
            {
                DownloadTask task = new DownloadTask(fileName, url, saveDir, callBack);
                tasks.Add(fileName, task);
            }
            return fileName;
        }
    }
    public class DownloadTask
    {
        private string fileName;
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
        private byte[] buffer = new byte[4096];
        public DownState GetStatus()
        {
            return state;
        }
        public float GetProcess()
        {
            if (fileLength > 0)
            {
                return (float)curLength / fileLength;
            }
            return 0;
        }
        public DownloadTask(string name, string url, string dir, Action callBack = null)
        {
            fileName = name;
            state = DownState.None;
            this.url = url;
            tempSavePath = dir + "/" + fileName + ".temp";
            savePath = dir + "/" + fileName;
            CallBack = callBack;
            DownTask();
        }

        void DownTask()
        {
            state = DownState.Start;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";

            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

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
            responseStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallBack), responseStream);
        }

        void ReadCallBack(IAsyncResult result)
        {
            responseStream = (Stream)result.AsyncState;
            int read = responseStream.EndRead(result);
            if (curLength < fileLength)
            {
                fileStream.Write(buffer, 0, read);
                curLength += read;
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
                HttpDownloader.Instance.RemoveTask(fileName);
            }
        }

    }
}
