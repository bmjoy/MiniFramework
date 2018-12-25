using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class NewBehaviourScript : MonoBehaviour {
	FileDownloader load;
	// Use this for initialization
	void Start () {
		string url ="https://vscode.cdn.azure.cn/stable/dea8705087adb1b5e5ae1d9123278e178656186a/VSCodeUserSetup-x64-1.30.1.exe";
		string path = Application.streamingAssetsPath;
		load = new FileDownloader(url,path);
		load.Download();
	}

	// Update is called once per frame
	void Update () {
		if(load.GetProcess()!=1){
			Debug.Log(load.GetProcess());
		}

	}

	void OnDestroy(){
		load.Close();
	}
}
