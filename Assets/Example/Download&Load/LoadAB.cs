using MiniFramework;
using UnityEngine;
public class LoadAB : MonoBehaviour {
    void Start () {      
        ResLoader.Instance.LoadAssetBundle(this, "file:///" + Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui", LoadCompleted);
    }
	void LoadCompleted(AssetBundle ab)
    {
        GameObject obj = ab.LoadAsset("UI Root") as GameObject;
        Instantiate(obj);
    }
}
