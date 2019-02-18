using System;
using UnityEngine;

public interface IResource {
    /// <summary>
    /// 异步加载Resource文件夹资源
    /// </summary>
    /// <param name="assetName"></param>
    void LoadAsset(string assetPath, Action<UnityEngine.Object> loadCallback);
    /// <summary>
    /// 异步加载AssetBundle资源
    /// </summary>
    /// <param name="assetBundlePath"></param>
    /// <param name="loadCallback"></param>
    void LoadAssetBundle(string assetBundlePath, string assetName, Action<UnityEngine.Object> loadCallback);
    void LoadAssetBundles(string assetBundlePath, Action<UnityEngine.Object[]> loadCallback);
    /// <summary>
    /// 卸载资源
    /// </summary>
    /// <param name="asset"></param>
    void UnloadAsset(UnityEngine.Object asset);
    /// <summary>
    /// 卸载无用资源
    /// </summary>
    void UnloadUnUsedAsset();
    void LoadScene(string sceneName);
    void LoadScene(int sceneIndex);    
    void UnloadScene(string sceneName);
    void UnloadScene(int sceneIndex);
}