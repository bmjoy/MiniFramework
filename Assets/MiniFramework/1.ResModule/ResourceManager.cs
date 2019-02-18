using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    public class ResourceManager : MonoBehaviour,IResource
    {
        public void LoadAsset(string assetPath, Action<UnityEngine.Object> loadCallback)
        {
            ResourceRequest request = Resources.LoadAsync(assetPath);
            StartCoroutine(requestIEnumerator(request, loadCallback));
        }

        public void LoadAssetBundle(string assetBundlePath,string assetName, Action<UnityEngine.Object> loadCallback)
        {
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(assetBundlePath);
            StartCoroutine(requestIEnumerator(request,assetName, loadCallback));
        }

        public void LoadAssetBundles(string assetBundlePath, Action<UnityEngine.Object[]> loadCallback)
        {
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(assetBundlePath);
            StartCoroutine(requestIEnumerator(request, loadCallback));
        }

        public void LoadScene(string sceneName)
        {
            throw new NotImplementedException();
        }

        public void UnloadUnUsedAsset()
        {
            Resources.UnloadUnusedAssets();
        }

        public void LoadScene(int sceneIndex)
        {
            throw new NotImplementedException();
        }

        public void UnloadAsset(UnityEngine.Object asset)
        {
            Resources.UnloadAsset(asset);
        }

        public void UnloadScene(string sceneName)
        {
            throw new NotImplementedException();
        }

        public void UnloadScene(int sceneIndex)
        {
            throw new NotImplementedException();
        }

        IEnumerator requestIEnumerator(ResourceRequest request, Action<UnityEngine.Object> loadCallback)
        {
            yield return request;
            if (request.isDone)
            {
                loadCallback(request.asset);
            }
        }
        IEnumerator requestIEnumerator(AssetBundleCreateRequest request,string assetName,Action<UnityEngine.Object> loadCallback)
        {
            yield return request;
            if (request.isDone)
            {
                AssetBundleRequest requset = request.assetBundle.LoadAssetAsync(assetName);
                yield return requset;
                loadCallback(requset.asset);
            }
        }
        IEnumerator requestIEnumerator(AssetBundleCreateRequest request, Action<UnityEngine.Object[]> loadCallback)
        {
            yield return request;
            if (request.isDone)
            {
                AssetBundleRequest requset = request.assetBundle.LoadAllAssetsAsync();
                yield return requset;
                loadCallback(requset.allAssets);
            }
        }
    }
}

