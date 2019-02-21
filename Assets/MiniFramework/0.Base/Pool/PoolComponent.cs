using System;
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
            public Stack<NeedPoolObject> Objs = new Stack<NeedPoolObject>();
        }
        public List<CacheObject> CacheObjects = new List<CacheObject>();
        public float DefaultWaitTime = 1f;//等待间隔时间

        public override void OnSingletonInit() { }

        public bool IsExist(string name)
        {
            CacheObject obj = GetCacheObject(name);
            return obj == null ? false : true;
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
        public void Init(GameObject obj, uint maxCount, uint minCount, bool destroyOnLoad)
        {
            if (obj == null || obj.GetComponent<NeedPoolObject>() == null)
            {
                return;
            }
            if (IsExist(obj.name))
            {
                return;
            }
            CacheObject cacheObj = new CacheObject();
            cacheObj.Name = obj.name;
            cacheObj.Max = maxCount;
            cacheObj.Min = minCount;
            cacheObj.DestroyOnLoad = destroyOnLoad;
            CacheObjects.Add(cacheObj);
            uint initCount = Math.Min(maxCount, minCount);
            for (int i = 0; i < initCount; i++)
            {
                GameObject temp = Instantiate(obj);
                temp.name = obj.name;
                Recycle(temp);
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
                if (cacheObject.Objs.Count>0)
                {
                    NeedPoolObject obj = cacheObject.Objs.Pop();
                    obj.IsRecycled = false;
                    obj.gameObject.SetActive(true);
                    obj.transform.SetParent(null);
                    
                    return obj.gameObject;
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
            obj.gameObject.SetActive(false);
            obj.gameObject.transform.SetParent(transform);

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
                cacheObj.Objs.Push(poolObject);
            }
            else
            {
                CacheObject newCache = new CacheObject();
                newCache.Name = obj.name;
                newCache.Objs.Push(poolObject);
                CacheObjects.Add(new CacheObject());
                poolObject.IsRecycled = true;
                poolObject.OnRecycled();
            }
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