using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

namespace MiniFramework
{
    public class ResLoader:Singleton<ResLoader>
    {
        private ResLoader() { }
        /// <summary>
        /// 获取所有AssetBundle的Hash128
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Dictionary<string,Hash128> GetAllAssetBundleHash(string path)
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
            AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] bundlesName = manifest.GetAllAssetBundles();
            Dictionary<string, Hash128> bundlesHash = new Dictionary<string, Hash128>();
            for (int i = 0; i < bundlesName.Length; i++)
            {
                Hash128 hash = manifest.GetAssetBundleHash(bundlesName[i]);
                bundlesHash.Add(bundlesName[i],hash);
            }
            return bundlesHash;
        }
        /// <summary>
        /// 异步加载Resources资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mono"></param>
        /// <param name="path"></param>
        /// <param name="loadedCallBack"></param>
        public void LoadAssetAsync<T>(MonoBehaviour mono, string path, Action<T> loadedCallBack) where T :UnityEngine.Object
        {
            mono.StartCoroutine(loadAsset<T>(path,loadedCallBack));
        }
        /// <summary>
        /// 异步加载AssetBundle包
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="path"></param>
        /// <param name="loadedCallBack"></param>
        public void LoadAssetBundle(MonoBehaviour mono, string path,Action<AssetBundle> loadedCallBack)
        {
            if (File.Exists(path))
                mono.StartCoroutine(loadAssetBundle(path, loadedCallBack));
        }
        public void UnLoadAssetBundle(AssetBundle ab){
            ab.Unload(false);
        }
        IEnumerator loadAsset<T>(string path, Action<T> loadedCallBack) where T : UnityEngine.Object
        {
            var resRequest = Resources.LoadAsync(path);
            yield return resRequest;
            var asset = resRequest.asset;
            if (asset == null)
            {
                Debug.LogError("Failed to load Asset!");
                yield break;
            }
            loadedCallBack(asset as T);
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
