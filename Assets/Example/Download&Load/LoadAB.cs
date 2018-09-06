using MiniFramework;
using System.Collections.Generic;
using UnityEngine;
public class LoadAB : MonoBehaviour
{
    void Start()
    {
        ResLoader.Instance.LoadAssetBundle(this, Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui", LoadCompleted);

        Dictionary<string,Hash128> bundlesHash = ResLoader.Instance.GetAllAssetBundleHash(Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/StandaloneWindows");
        foreach (var item in bundlesHash)
        {
            Debug.Log(item.Key+":"+item.Value);
        }
    } 
    void LoadCompleted(AssetBundle ab)
    {
        GameObject obj = ab.LoadAsset<GameObject>("UI Panel");
        Instantiate(obj);
        ab.Unload(false);
    }
}
