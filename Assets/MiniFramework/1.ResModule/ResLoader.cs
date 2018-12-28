using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace MiniFramework
{
    public class ResLoader : MonoSingleton<ResLoader>
    {
        private ResLoader() { }
        /// <summary>
        /// 获取所有AssetBundle的Hash128
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Dictionary<string, Hash128> GetAllAssetBundleHash(string path)
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
            AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] bundlesName = manifest.GetAllAssetBundles();
            Dictionary<string, Hash128> bundlesHash = new Dictionary<string, Hash128>();
            for (int i = 0; i < bundlesName.Length; i++)
            {
                Hash128 hash = manifest.GetAssetBundleHash(bundlesName[i]);
                bundlesHash.Add(bundlesName[i], hash);
            }
            return bundlesHash;
        }
        /// <summary>
        /// 异步加载AssetBundle包
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="path"></param>
        /// <param name="loadedCallBack"></param>
        public void LoadAssetBundle<T>(string path, string assetName,Action<T> callback) where T :UnityEngine.Object
        {
            if (File.Exists(path))
                StartCoroutine(loadAssetBundle<T>(path, assetName, callback));
        }
        IEnumerator loadAssetBundle<T>(string loadPath, string assetName,Action<T> callback) where T : UnityEngine.Object
        {
            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(loadPath);
            yield return bundleLoadRequest;
            var loadedAssetBundle = bundleLoadRequest.assetBundle;
            if (loadedAssetBundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                yield break;
            }
            AssetBundleRequest requset = loadedAssetBundle.LoadAssetAsync<T>(assetName);
            yield return requset;
            T obj = requset.asset as T;
            callback(obj);
            loadedAssetBundle.Unload(false);
        }

        /// <summary>
        /// 异步加载AssetBundle包
        /// 所有资源
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="path"></param>
        /// <param name="loadedCallBack"></param>
        public void LoadAllAssetBundle<T>(string path, Action<T[]> callback) where T : UnityEngine.Object
        {
            if (File.Exists(path))
                StartCoroutine(loadAllAssetBundle(path, callback));
        }
        IEnumerator loadAllAssetBundle<T>(string loadPath, Action<T[]> callback) where T : UnityEngine.Object
        {
            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(loadPath);
            yield return bundleLoadRequest;
            var loadedAssetBundle = bundleLoadRequest.assetBundle;
            if (loadedAssetBundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                yield break;
            }
            AssetBundleRequest requset = loadedAssetBundle.LoadAllAssetsAsync();
            yield return requset;
            T[] obj = requset.allAssets as T[];
            callback(obj);
            loadedAssetBundle.Unload(false);
        }
    }
}