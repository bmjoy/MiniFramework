using UnityEngine;
using System.Collections;
using System;

namespace MiniFramework
{
    public class ResLoader:Singleton<ResLoader>
    {
        private ResLoader() { }


        public void LoadAssetAsync(MonoBehaviour mono, string path, Action<UnityEngine.Object> loadedCallBack)
        {
            mono.StartCoroutine(loadAsset(path,loadedCallBack));
        }
        IEnumerator loadAsset(string path, Action<UnityEngine.Object> loadedCallBack)
        {
            var resRequest = Resources.LoadAsync(path);
            yield return resRequest;
            var asset = resRequest.asset;
            if (asset == null)
            {
                Debug.LogError("Failed to load Asset!");
                yield break;
            }
            loadedCallBack(asset);
        }
        public void LoadAssetBundle(MonoBehaviour mono, string path,Action<AssetBundle> loadedCallBack)
        {
            mono.StartCoroutine(loadAssetBundle(path, loadedCallBack));
        }

        IEnumerator loadAssetBundle(string loadPath, Action<AssetBundle> loadedCallBack)
        {
            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(loadPath);
            yield return bundleLoadRequest;
            var loadedAssetBundle = bundleLoadRequest.assetBundle;
            if (loadedAssetBundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                yield break;
            }
            loadedCallBack(loadedAssetBundle);
        }
    }
}
