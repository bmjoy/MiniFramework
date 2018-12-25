using System;
using System.IO;
using System.Net;
using UnityEngine;
namespace MiniFramework
{
    public class FileDownloader
    {
        private string url;
        private string saveDir;
        public string FileName;
        private Stream responseStream;
        private FileStream fileStream;
        private long curLength;
        private long fileLength;
        private byte[] buffer = new byte[1024];
        public FileDownloader(string url, string saveDir)
        {
            this.url = url;
            this.saveDir = saveDir;
            FileName = GetFileNameFromUrl(url);
        }
        public FileDownloader(string url, string saveDir, string fileName)
        {
            this.url = url;
            this.saveDir = saveDir;
            FileName = fileName;
        }
        private string GetFileNameFromUrl(string url)
        {
            FileName = url.Substring(url.LastIndexOf('/') + 1);
            return FileName;
        }
        public float GetProcess()
        {
            if (fileLength > 0)
            {
                return (float)curLength / fileLength;
            }
            return 0;
        }
        public void Download()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            string tempSavePath = saveDir + "/" + FileName + ".temp";
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
        private void RespCallBack(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
            responseStream = response.GetResponseStream();
            fileLength = response.ContentLength + curLength;
            responseStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), responseStream);
        }
        private void ReadCallback(IAsyncResult result)
        {
            responseStream = (Stream)result.AsyncState;
            int read = responseStream.EndRead(result);
            if (curLength < fileLength)
            {
                fileStream.Write(buffer, 0, read);
                curLength += read;
                responseStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), responseStream);
            }
            else
            {
                Close();
                string tempSavePath = saveDir + "/" + FileName + ".temp";
                string savePath = saveDir + "/" + FileName;
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                File.Move(tempSavePath, savePath);
                Debug.Log("下载完成");
            }
        }

        public void Close()
        {
            fileStream.Close();
            responseStream.Close();
        }
    }
}
