using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace MiniFramework
{
    public class Binary
    {
        public static void Serialize(object obj, string path)
        {
            if (obj == null)
            {
                throw new System.Exception("obj不能为空！");
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new System.Exception("路径不能为空！");
            }
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
        }
        public static T Deserialize<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new System.Exception("文件不存在！：" + path);
            }
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object data = bf.Deserialize(fs);
                return (T)data;
            }
        }
    }
}

