using UnityEngine;
using MiniFramework;
public class UsePool : MonoBehaviour {
    public GameObject Prefab;
	// Use this for initialization
	void Start () {
        ObjectPool.Instance.Init(Prefab, 5,5,true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ObjectPool.Instance.Allocate(Prefab.name);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(ObjectPool.Instance.CurCount(Prefab.name));
        }
    }
}
