using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class ResLoaderExample : MonoBehaviour
{

    // Use this for initialization
    IEnumerator Start()
    {
        string path = Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/testab";
        ResLoader.Instance.LoadAllAssetBundle(path, LoadCallback);
        yield return new WaitForEndOfFrame();//等待上一帧AssetBundle释放 不然会报错
        ResLoader.Instance.LoadAssetBundle(path, "Sphere", LoadCallback);
    }
    public void LoadCallback(Object[] objs)
    {
        GameObject obj = Instantiate(objs[0]) as GameObject;
        Debug.Log(obj.name);

    }
    public void LoadCallback(Object obj)
    {
        GameObject gobj = Instantiate(obj) as GameObject;
        Debug.Log(gobj.name);
    }
}
