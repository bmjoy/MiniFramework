using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using System.IO;

public class ZipExample : MonoBehaviour {
	// Use this for initialization
	void Start () {
        ZipUtil.ZipDirectory(Application.streamingAssetsPath,Application.dataPath,"Test.zip");
        Debug.Log("压缩完成");
        ZipUtil.UpZipFile(Application.dataPath + "/Test.zip", Application.dataPath+"/Test");
        Debug.Log("解压完成");
    }
}
