using System;
using System.IO;
using System.Net;
namespace MiniFramework
{
    public class Downloader : IDownloader
    {    
        public event Action<string> DownloadSuccessed;
        public event Action<string> DownloadFailed;

        private string fileName;

        private int bufferSize = 1024 * 1024;
        private int timeout = 10000;
        private long curLength;
        private long fileLength;

        private byte[] buffer;
        private string url;
        private string saveDir;
        private string tempSavePath;

        private DownloadState state;
        private FileStream fileStream;
        private Stream responseStream;     
        public Downloader(string url,string saveDir)
        {
            this.url = url;
            this.saveDir = saveDir;
            fileName = GetFileNameFromUrl(url);
            state = DownloadState.None;
        }
        public string GetFileName()
        {
            return fileName;
        }
        public long GetCurLength()
        {
            return curLength;
        }
        public float GetDownloadProcess()
        {
            return fileLength > 0 ? (float)curLength / fileLength : 0;
        }
        public DownloadState GetDownloadSate()
        {
            return state;
        }
        // Use this for initialization
        public void Start()
        {           
            tempSavePath = saveDir + "/" + fileName + ".temp";
            
            HttpRequest();
        }

        public void Close()
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }  
            if (responseStream != null)
            {
                responseStream.Close();
            }
            if (state == DownloadState.Downloading)
            {
                state = DownloadState.None;
            }              
        }
        private void HttpRequest()
        {
            try
            {
                state = DownloadState.Downloading;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = timeout;
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
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
                request.BeginGetResponse(new AsyncCallback(RespCallback), request);
            }
            catch(Exception e)
            {
                DownloadFailed(fileName);
                throw e;
            }  
        }
        private void RespCallback(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            if (!request.HaveResponse)
            {
                DownloadFailed(fileName);
                state = DownloadState.Failed;
                Close();
                return;
            }
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
            WebHeaderCollection collection = response.Headers;
            responseStream = response.GetResponseStream();
            fileLength = response.ContentLength + curLength;
            buffer = new byte[bufferSize];
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
                buffer = null;
                state = DownloadState.Completed;
                Close();
                string savePath = saveDir + "/" + fileName;
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                File.Move(tempSavePath, savePath);
                DownloadSuccessed(fileName);
            }
        }

        private string GetFileNameFromUrl(string url)
        {
            fileName = url.Substring(url.LastIndexOf('/') + 1);
            return fileName;
        }
    }
}

