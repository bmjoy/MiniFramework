using MiniFramework;
using UnityEngine;
public class LoadAB : MonoBehaviour {
    void Start () {      
        ResLoader rl = new ResLoader(this, "file:///" + Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui");
        rl.LoadAssetBundle(LoadCompleted);
    }
	void LoadCompleted(AssetBundle ab)
    {
        GameObject obj = ab.LoadAsset("UI Root") as GameObject;
        Instantiate(obj);
    }
}
