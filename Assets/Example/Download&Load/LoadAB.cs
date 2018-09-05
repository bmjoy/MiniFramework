using MiniFramework;
using UnityEngine;
public class LoadAB : MonoBehaviour
{
    void Start()
    {
        ResLoader.Instance.LoadAssetBundle(this, Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui", LoadCompleted);

        Hash128[] hashes = ResLoader.Instance.GetAllAssetBundleHash(Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/StandaloneWindows");
        for (int i = 0; i < hashes.Length; i++)
        {
            Debug.Log(hashes[i]);
        }

    } 
    void LoadCompleted(AssetBundle ab)
    {
        GameObject obj = ab.LoadAsset<GameObject>("UI Panel");
        Instantiate(obj);
        ab.Unload(false);
    }
}
