using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniFramework
{
    public class PoolComponent : MonoSingleton<PoolComponent>
    {
        [Serializable]
        public class CacheObject
        {
            public string Name;
            public uint Max;
            public uint Min;
            public bool DestroyOnLoad;
            public GameObject Prefab;
            public Stack<NeedPoolObject> Objs = new Stack<NeedPoolObject>();
        }
        [HideInInspector]
        public List<CacheObject> CacheObjects = new List<CacheObject>();
        public float DefaultWaitTime = 0.2f;//等待间隔时间
        public override void OnSingletonInit() { }
        IEnumerator Start()
        {
            for (int i = 0; i < CacheObjects.Count; i++)
            {
                yield return new WaitForSeconds(DefaultWaitTime);
                Init(CacheObjects[i].Prefab,CacheObjects[i].Max,CacheObjects[i].Min,CacheObjects[i].DestroyOnLoad);
            }
        }
        public bool IsExist(string name)
        {
            return GetCacheObject(name)==null ? false : true;
        }
        public CacheObject GetCacheObject(string name)
        {
            foreach (var item in CacheObjects)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 该对象当前缓存数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int CurCount(string name)
        {
            foreach (var item in CacheObjects)
            {
                if (item.Name == name)
                {
                    return item.Objs.Count;
                }
            }
            return 0;
        }
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="count"></param>
        public void Init(GameObject obj, uint maxCount, uint minCount, bool destroyOnLoad=false)
        {
            if (obj == null || obj.GetComponent<NeedPoolObject>() == null)
            {
                return;
            }
            uint initCount = Math.Min(maxCount, minCount);
            CacheObject cacheObj = GetCacheObject(obj.name);
            if (cacheObj == null)
            {
                cacheObj = new CacheObject();
                CacheObjects.Add(cacheObj);
            }
            cacheObj.Name = obj.name;
            cacheObj.Max = maxCount;
            cacheObj.Min = minCount;
            cacheObj.DestroyOnLoad = destroyOnLoad;
            cacheObj.Prefab = obj;
            for (int i = cacheObj.Objs.Count; i < initCount; i++)
            {
                GameObject cloneObj = Instantiate(obj);
                cloneObj.name = obj.name;
                if (!destroyOnLoad)
                {
                    DontDestroyOnLoad(cloneObj);
                }
                Recycle(cloneObj);
            }
        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public GameObject Allocate(string objName)
        {
            CacheObject cacheObject = GetCacheObject(objName);
            if (cacheObject != null)
            {
                if (cacheObject.Objs.Count > 0)
                {
                    NeedPoolObject obj = cacheObject.Objs.Pop();
                    if (obj != null)
                    {
                        obj.IsRecycled = false;
                        obj.gameObject.SetActive(true);
                        return obj.gameObject;
                    }
                }
            }
            Debug.LogError("该对象不存在缓存池中!");
            return null;
        }
        public bool Recycle(GameObject obj)
        {
            if (obj == null)
            {
                return false;
            }
            NeedPoolObject poolObject = obj.GetComponent<NeedPoolObject>();
            if (poolObject == null || poolObject.IsRecycled)
            {
                return false;
            }
            CacheObject cacheObj = GetCacheObject(obj.name);
            if (cacheObj != null)
            {
                if (cacheObj.Objs.Count >= cacheObj.Max)
                {
                    return false;
                }
                if (cacheObj.Objs.Contains(poolObject))
                {
                    return false;
                }
            }
            else
            {
                cacheObj = new CacheObject();
                cacheObj.Name = obj.name;
                CacheObjects.Add(new CacheObject());
            }
            cacheObj.Objs.Push(poolObject);
            poolObject.IsRecycled = true;
            poolObject.OnRecycled();
            obj.gameObject.SetActive(false);
            return true;
        }
        public void Recycle(string name)
        {
            NeedPoolObject[] objs = GameObject.FindObjectsOfType<NeedPoolObject>();
            foreach (var item in objs)
            {
                if (item.name == name)
                {
                    Recycle(item.gameObject);
                }
            }
        }

    }
}