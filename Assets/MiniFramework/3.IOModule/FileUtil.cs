using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace MiniFramework
{
    public class FileUtil
    {
        private static byte[] buffer;
        private static event Action writeCallback;
        private static event Action<byte[]> readCallback;
        public static void CreateDirectoryIfNoExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public static void WriteToLocalAsync(string content, string path,Action callback)
        {
            byte[] data = Encoding.UTF8.GetBytes(content);
            WriteToLocalAsync(data,path,callback);
        }
        public static void WriteToLocalAsync(byte[] data, string path,Action callback)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                writeCallback = callback;
                stream.BeginWrite(data, 0, data.Length, WriteCallback, stream);
            }
        }

        private static void WriteCallback(IAsyncResult ar)
        {
            using (FileStream stream = (FileStream)ar.AsyncState)
            {
                stream.EndWrite(ar);
                Debug.Log("写入完成");
                writeCallback(); 
            }
        }
        public static void ReadFromLocalAsync(string path, Action<byte[]> callback)
        {
            using( FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                readCallback = callback;
                buffer = new byte[stream.Length];
                stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, stream);
            }
        }
        private static void ReadCallback(IAsyncResult ar)
        {
            using (FileStream stream = (FileStream)ar.AsyncState)
            {
                stream.EndRead(ar);               
                Debug.Log("读取完成");
                readCallback(buffer);  
                buffer =null;
            }
        }
        public static byte[] ReadFromLocal(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}