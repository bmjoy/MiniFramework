using MiniFramework;
using UnityEngine;
public class LoadAB : MonoBehaviour {
    void Start () {      
        ResLoader.Instance.LoadAssetBundle(this,Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui", LoadCompleted);
    }
	void LoadCompleted(AssetBundle ab)
    {
        GameObject obj = ab.LoadAsset<GameObject>("UI Root");
        Instantiate(obj);
        ab.Unload(false);
    }
}
