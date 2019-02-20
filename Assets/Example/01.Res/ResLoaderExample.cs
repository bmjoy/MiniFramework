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
    private AudioSource aSource;
    // Use this for initialization
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        ResourceManager.Instance.AssetLoader.LoadAssetBundles(Application.streamingAssetsPath + ABPath, LoadCallback);
        Dictionary<string,Hash128> manifest = ResourceManager.Instance.AssetLoader.LoadABManifest(Application.streamingAssetsPath+ManifestPath);
        foreach (var item in manifest)
        {
            Debug.Log(item.Key + ":" + item.Value);
        }

        // ResourceManager.Instance.SceneLoader.LoadScene(SceneName,LoadCallback);

        ResourceManager.Instance.AssetLoader.LoadAsset(ResourcePath,LoadCallback);
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
        aSource.PlayOneShot (obj as AudioClip);
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
