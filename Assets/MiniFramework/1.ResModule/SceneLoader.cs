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
        public void LoadScene(string sceneName, Action<AsyncOperation> loadCallback)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
            monoBehaviour.StartCoroutine(requestIEnumerator(async, loadCallback));
        }
        public void LoadScene(int sceneIndex, Action<AsyncOperation> loadCallback)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
            monoBehaviour.StartCoroutine(requestIEnumerator(async, loadCallback));
        }
        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        public void UnloadScene(int sceneIndex)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
        IEnumerator requestIEnumerator(AsyncOperation async, Action<AsyncOperation> loadCallback)
        {
            async.allowSceneActivation = false;
            while (!async.isDone)
            {
                if (async.progress >= 0.9f)
                {
                    async.allowSceneActivation = true;
                    if (loadCallback != null)
                    {
                        loadCallback(async);
                    }
                }
                yield return null;
            }
        }
    }
}