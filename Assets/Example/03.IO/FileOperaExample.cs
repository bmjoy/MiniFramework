using UnityEngine;
using MiniFramework;
using System.Text;

public class FileOperaExample : MonoBehaviour {
    public string content;
	// Use this for initialization
	void Start () {
        FileUtil.SaveToLocalAsync("hello world", Application.streamingAssetsPath + "/bin");
        content = FileUtil.ReadTextFromLocal(Application.streamingAssetsPath + "/bin");
    }
}
