using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		if(Time.frameCount%2==0){
			Debug.Log("Log");
			Debug.LogWarning("Warning");
			Debug.LogError("Error");
			Debug.LogAssertion("Assert");
			throw new System.Exception("Exception");
		}
	}
}
