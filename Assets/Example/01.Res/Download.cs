using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class Download : MonoBehaviour
{
    FileDownloader load;
    // Use this for initialization
    void Start()
    {
        string url = "http://download.unity3d.com/download_unity/6e9a27477296/TargetSupportInstaller/UnitySetup-Android-Support-for-Editor-2018.3.0f2.exe";
        string path = Application.streamingAssetsPath;
        load = new FileDownloader(url, path);
        load.Download();
    }

    // Update is called once per frame
    void Update()
    {
        if (load.GetProcess() > 0 && load.GetProcess() < 1)
        {
            Debug.Log(load.GetProcess());
        }
    }

    void OnDestroy()
    {
        load.Close();
    }
}
