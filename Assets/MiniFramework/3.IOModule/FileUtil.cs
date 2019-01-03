using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace MiniFramework
{
    public class FileUtil
    {
        public static void CreateDirectoryIfNoExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public static void SaveToLocalAsync(string content, string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes(content);
                stream.BeginWrite(data, 0, data.Length, SaveCallback, stream);
            }
        }
        public static void SaveToLocalAsync(byte[] content, string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.BeginWrite(content, 0, content.Length, SaveCallback, stream);
            }
        }
        private static void SaveCallback(IAsyncResult ar)
        {
            using (FileStream str = (FileStream)ar.AsyncState)
            {
                str.EndWrite(ar);
                Debug.Log("写入完成");
            }
        }
        public static string ReadTextFromLocal(string path)
        {
            return File.ReadAllText(path);
        }
        public static byte[] ReadBytesFromLocal(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}