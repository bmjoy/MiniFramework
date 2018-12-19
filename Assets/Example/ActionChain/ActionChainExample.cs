using UnityEngine;
using MiniFramework;
public class ActionChainExample : MonoBehaviour {
    // Use this for initialization
    void Start() {
        this.Delay(2f, () => { Debug.Log("延迟2s执行"); });

        this.Sequence().Until(() => { return Time.time > 2f; }).Event(() => { Debug.Log("Hello"); }).Delay(2).Event(() => { Debug.Log(Time.time); }).Execute();       
    }
}
