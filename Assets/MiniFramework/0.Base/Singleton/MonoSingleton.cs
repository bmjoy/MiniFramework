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
                        obj.AddComponent<T>();
                    }
                }
                return mInstance;
            }
        }
        protected virtual void Awake()
        {
            if (mInstance == null)
            {
                mInstance = this as T;
                DontDestroyOnLoad(this);
                mInstance.OnSingletonInit();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        protected virtual void OnSingletonInit(){}
        protected virtual void Dispose()
        {
            DestroyImmediate(mInstance.gameObject);
        }
    }
}