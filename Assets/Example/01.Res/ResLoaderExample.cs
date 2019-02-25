using UnityEngine;
using MiniFramework;
using System;
using System.Collections.Generic;

public class ResLoaderExample : MonoBehaviour
{
    public string ABPath;
    public string ManifestPath;
    public string ResourcePath;
    public string SceneName;
    // Use this for initialization
    void Start()
    {
        ResourceManager.Instance.AssetLoader.LoadAsset(ResourcePath, LoadCallback);
        ResourceManager.Instance.AssetLoader.LoadAssetBundles(Application.streamingAssetsPath + ABPath, LoadCallback);
        Dictionary<string,Hash128> manifest = ResourceManager.Instance.AssetLoader.LoadABManifest(Application.streamingAssetsPath+ManifestPath);
        foreach (var item in manifest)
        {
            Debug.Log(item.Key + ":" + item.Value);
        }
    }
    public void LoadCallback(UnityEngine.Object[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject obj = Instantiate(objs[i]) as GameObject;
            Debug.Log(obj.name);
        }
    }
    public void LoadCallback(UnityEngine.Object obj)
    {
        Instantiate(obj);
        Debug.Log(obj.name);
    }
    public void LoadCallback(AsyncOperation async)
    {
        Debug.Log(async.progress);
        if (async.progress >= 0.9f)
        {
            async.allowSceneActivation = true;
        }
    }
}
