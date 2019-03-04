using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class ResourceExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ResourceManager.Instance.SceneLoader.LoadSceneAsync("A",Loading);
	}
	
	// Update is called once per frame
	void Loading(AsyncOperation async){
		Debug.Log(async.progress);
	}
}
