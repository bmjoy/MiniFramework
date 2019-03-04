using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniFramework
{
    public class SceneLoader
    {
        private MonoBehaviour monoBehaviour;
        public SceneLoader(MonoBehaviour mono)
        {
            monoBehaviour = mono;
        }
        public void LoadSceneAsync(string sceneName, Action<AsyncOperation> loadingCallback = null)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
            if (loadingCallback != null)
            {
                monoBehaviour.StartCoroutine(requestIEnumerator(async, loadingCallback));
            }
        }
        public void LoadSceneAsync(int sceneIndex, Action<AsyncOperation> loadingCallback = null)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
            if (loadingCallback != null)
            {
                monoBehaviour.StartCoroutine(requestIEnumerator(async, loadingCallback));
            }
        }
        public void UnloadSceneAsync(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        public void UnloadSceneAsync(int sceneIndex)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
        IEnumerator requestIEnumerator(AsyncOperation async, Action<AsyncOperation> loadingCallback)
        {
            while (!async.isDone)
            {
                loadingCallback(async);
                yield return null;
            }
        }
    }
}