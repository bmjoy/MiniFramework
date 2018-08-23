namespace MiniFramework
{
    using UnityEngine;
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T mInstance = null;
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = Object.FindObjectOfType<T>();
                    if (mInstance == null)
                    {
                        var obj = new GameObject(typeof(T).Name);                       
                        mInstance = obj.AddComponent<T>();
                        DontDestroyOnLoad(obj);
                        mInstance.OnSingletonInit();
                    }
                }
                return mInstance;
            }        
        }
        public virtual void OnSingletonInit()
        {

        }
        public virtual void Dispose()
        {
            DestroyImmediate(mInstance.gameObject);
        }
    }
}