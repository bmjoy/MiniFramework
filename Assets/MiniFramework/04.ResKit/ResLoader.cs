using UnityEngine;
using System.Collections;
using System;

namespace MiniFramework
{
    public class ResLoader
    {
        private string loadPath;
        private MonoBehaviour mono;
        private Action<AssetBundle> callBack;

        public ResLoader(MonoBehaviour self,string path)
        {
            mono = self;
            loadPath = path;
        }

        public void LoadAssetBundle(Action<AssetBundle> callBack)
        {           
            mono.StartCoroutine(loadAssetBundle(callBack));
        }

        IEnumerator loadAssetBundle(Action<AssetBundle> callBack)
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
