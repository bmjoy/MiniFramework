using System.IO;
using System.Xml.Serialization;

namespace MiniFramework
{
    public class Xml
    {
        public static void Serialize(object obj,string path)
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
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                xml.Serialize(fs, obj);
            }
        }
        public static T Deserialize<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new System.Exception("路径不能为空！");
            }
            if (!File.Exists(path))
            {
                throw new System.Exception("文件不存在！:" + path);
            }
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                object data = xml.Deserialize(fs);
                return (T)data;
            }
        }        
    }
}

