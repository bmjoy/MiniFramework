using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class ActionChainExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.Sequence().Delay(1).Event(()=>{Debug.Log("A");}).Until(()=>Input.GetKeyDown(KeyCode.Space)).Event(()=>{Debug.Log("按下空格");}).Execute();
	}
}
