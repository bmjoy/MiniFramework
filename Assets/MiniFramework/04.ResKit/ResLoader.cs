using UnityEngine;
using System.Collections;
using System;

namespace MiniFramework
{
    public class ResLoader:Singleton<ResLoader>
    {
        private string loadPath;
        private MonoBehaviour mono;
        private Action<AssetBundle> callBack;
        private ResLoader() { }
        public void LoadAssetBundle(MonoBehaviour mono, string path,Action<AssetBundle> callBack)
        {
            this.mono = mono;
            this.loadPath = path;
            this.callBack = callBack;
            this.mono.StartCoroutine(loadAssetBundle());
        }

        IEnumerator loadAssetBundle()
        {
            WWW www = new WWW(loadPath);
            yield return www;
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield break;
            }
            callBack(www.assetBundle);
            www.Dispose();
        }
    }
}
