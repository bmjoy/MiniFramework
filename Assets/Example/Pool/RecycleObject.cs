using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class RecycleObject : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        Invoke("Recycle", 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Recycle()
    {
        ObjectPool.Instance.Recycle(gameObject);
    }
}
