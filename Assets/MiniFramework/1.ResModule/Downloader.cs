using System;
using System.IO;
using System.Net;
namespace MiniFramework
{
    public class Downloader : IDownloader
    {

        public string FileName;
        public event Action<string> DownloadSuccessed;
        public event Action<string> DownloadFailed;

        public int BufferSize = 1024 * 1024;
        public int Timeout = 10000;
        public long CurLength;
        public long FileLength;

        private byte[] buffer;
        private string url;
        private string saveDir;
        private string tempSavePath;
             
        private FileStream fileStream;
        private Stream responseStream;
      
        public Downloader(string url,string saveDir)
        {
            this.url = url;
            this.saveDir = saveDir;
            FileName = GetFileNameFromUrl(url);
        }
        public float GetDownloadProcess()
        {
            return FileLength > 0 ? (float)CurLength / FileLength : 0;
        }
        // Use this for initialization
        public void Start()
        {           
            tempSavePath = saveDir + "/" + FileName + ".temp";
            HttpRequest();
        }

        public void Close()
        {
            if (fileStream != null)
                fileStream.Close();
            if (responseStream != null)
                responseStream.Close();
        }
        private void HttpRequest()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = Timeout;
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            if (File.Exists(tempSavePath))
            {
                fileStream = File.OpenWrite(tempSavePath);
                CurLength = fileStream.Length;
                fileStream.Seek(CurLength, SeekOrigin.Current);
                request.AddRange((int)CurLength);
            }
            else
            {
                fileStream = new FileStream(tempSavePath, FileMode.Create, FileAccess.Write);
                CurLength = 0;
            }
            request.KeepAlive = false;
            request.BeginGetResponse(new AsyncCallback(RespCallback), request);
        }
        private void RespCallback(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            if (!request.HaveResponse)
            {
                DownloadFailed(FileName);
                return;
            }
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
            responseStream = response.GetResponseStream();
            FileLength = response.ContentLength + CurLength;
            buffer = new byte[BufferSize];
            responseStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), responseStream);
        }
        private void ReadCallback(IAsyncResult result)
        {
            responseStream = (Stream)result.AsyncState;
            int read = responseStream.EndRead(result);
            if (CurLength < FileLength)
            {
                fileStream.Write(buffer, 0, read);
                CurLength += read;
                responseStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), responseStream);
            }
            else
            {
                Close();
                string savePath = saveDir + "/" + FileName;
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                File.Move(tempSavePath, savePath);
                DownloadSuccessed(FileName);
            }
        }

        private string GetFileNameFromUrl(string url)
        {
            FileName = url.Substring(url.LastIndexOf('/') + 1);
            return FileName;
        }
    }
}

