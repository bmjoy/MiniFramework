using MiniFramework;
using System;
using UnityEngine;
public class DebugTest : MonoBehaviour {
	// Use this for initialization
	void Start () {
       // MiniFramework.Console.Instance.OnSingletonInit();
        Debug.Log("xxxx");
        throw new Exception("error");
    }	
}
