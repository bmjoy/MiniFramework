using UnityEngine;
using System.Collections;
using System;

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
        public Hash128[] GetAllAssetBundleHash(string path)
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
            AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] bundlesName = manifest.GetAllAssetBundles();
            Hash128[] bundlesHash = new Hash128[bundlesName.Length];
            for (int i = 0; i < bundlesName.Length; i++)
            {
                bundlesHash[i] = manifest.GetAssetBundleHash(bundlesName[i]);
            }
            return bundlesHash;
        }
        /// <summary>
        /// 从Resources文件异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mono"></param>
        /// <param name="path"></param>
        /// <param name="loadedCallBack"></param>
        public void LoadAssetAsync<T>(MonoBehaviour mono, string path, Action<T> loadedCallBack) where T :UnityEngine.Object
        {
            mono.StartCoroutine(loadAsset<T>(path,loadedCallBack));
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
        /// <summary>
        /// 异步加载AssetBundle
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="path"></param>
        /// <param name="loadedCallBack"></param>
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
