using MiniFramework;
using UnityEngine;

public class ActionKitExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.Delay(2f, () => { Debug.Log("延迟2s执行"); });
        this.Sequence()
            .Delay(1)
            .Event(() => Debug.Log("1s后开始队列"))
            .Until(() => Input.GetKeyDown(KeyCode.Space))
            .Event(()=> Debug.Log("按下空格"))
            .Start();
	}
}
