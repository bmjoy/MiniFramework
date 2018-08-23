using System.IO;
namespace MiniFramework
{
    public static class FileUtil
    {
        public static void CreateDirectoryIfNoExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        public static void SaveBinaryToLocal(byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }
        public static byte[] ReadBinaryFromLocal(string path)
        {
            return File.ReadAllBytes(path);
        }
        public static void SaveTextToLocal(string content, string path) 
        {
            File.WriteAllText(path,content);
        }

        public static string ReadTextFromLocal(string path)
        {
            return File.ReadAllText(path);
        }
    }
}