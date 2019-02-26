using UnityEngine;
using MiniFramework;
using System.Text;
public class FileOperaExample : MonoBehaviour
{
    // Use this for initialization
    string path;
    byte[] data;
    void Start()
    {
        path = Application.streamingAssetsPath + "/bin";
    }
    void OnGUI()
    {
        GUILayout.Label(Time.time.ToString());
        if (GUILayout.Button("Write"))
        {            
            data = new byte[1024*1024*50];//50MB的文件大小
            FileUtil.WriteToLocalAsync(data, path, null);
        }
        if (GUILayout.Button("Read"))
        {
            FileUtil.ReadFromLocalAsync(path, readCallback);
        }
    }
    void readCallback(byte[] data)
    {
        //Debug.Log(UTF8Encoding.UTF8.GetString(data));
    }
}
