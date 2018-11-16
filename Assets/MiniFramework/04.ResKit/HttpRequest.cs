using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace MiniFramework
{
    public static class HttpRequest
    {
        public static void HttpGet(this MonoBehaviour mono, string url, Action<string> callback)
        {
            mono.StartCoroutine(GetEnumerator(url, callback));
        }
        static IEnumerator GetEnumerator(string url, Action<string> callback)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.Send();

                if (www.isError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    callback(www.downloadHandler.text);
                }
            }
        }

        public static void HttpPost(this MonoBehaviour mono, string url, WWWForm form, Action<string> callback)
        {
            mono.StartCoroutine(GetEnumerator(url, callback));
        }
        static IEnumerator PostEnumerator(string url, WWWForm form, Action<string> callback)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(url,form))
            {
                yield return www.Send();

                if (www.isError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    callback(www.downloadHandler.text);
                }
            }
        }
    }
}

