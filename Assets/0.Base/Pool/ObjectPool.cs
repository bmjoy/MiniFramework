using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniFramework
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [Serializable]
        public struct Prefab
        {
            public GameObject Obj;
            public uint Max;
            public uint Min;
            public bool DestroyOnLoad;
        }
        public List<Prefab> NeedCachePrefabs = new List<Prefab>();
        private Dictionary<string, Stack<GameObject>> mCacheDict = new Dictionary<string, Stack<GameObject>>();
        private uint mMaxCount = 10;//缓存池最大个数
        /// <summary>
        /// 该对象当前缓存数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int CurCount(string name)
        {
            if (mCacheDict.ContainsKey(name))
            {
                return mCacheDict[name].Count;
            }
            return 0;
        }
        private void Start()
        {
            for (int i = 0; i < NeedCachePrefabs.Count; i++)
            {
                Init(NeedCachePrefabs[i].Obj, NeedCachePrefabs[i].Max, NeedCachePrefabs[i].Min, NeedCachePrefabs[i].DestroyOnLoad);
            }
        }
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="count"></param>
        public void Init(GameObject obj, uint maxCount, uint minCount, bool DestroyOnLoad = false)
        {
            if (obj == null)
            {
                return;
            }
            mMaxCount = maxCount;
            uint initCount = Math.Min(maxCount, minCount);
            if (CurCount(obj.name) < initCount)
            {
                for (int i = CurCount(obj.name); i < initCount; i++)
                {
                    GameObject temp = Instantiate(obj);
                    if (!DestroyOnLoad)
                    {
                        DontDestroyOnLoad(temp);
                    }
                    temp.name = obj.name;
                    Recycle(temp);
                }
            }
        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public GameObject Allocate(string objName)
        {
            if (mCacheDict.ContainsKey(objName))
            {
               return Pop(objName);
            }
            return null;
        }
        private GameObject Pop(string key)
        {
            if (mCacheDict[key].Count > 0)
            {
                GameObject obj = mCacheDict[key].Pop();
                if (obj == null)
                {
                   obj = Pop(key);
                }
                obj.SetActive(true);
                return obj;
            }
            return null;
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Recycle(GameObject obj)
        {
            if (obj == null)
            {
                return false;
            }
            obj.SetActive(false);
            if (mCacheDict.ContainsKey(obj.name))
            {
                if (mCacheDict[obj.name].Count >= mMaxCount)
                {
                    return false;
                }
                if (mCacheDict[obj.name].Contains(obj))
                {
                    return false;
                }
                mCacheDict[obj.name].Push(obj);
            }
            else
            {
                mCacheDict.Add(obj.name, new Stack<GameObject>());
                mCacheDict[obj.name].Push(obj);
            }
            return true;
        }
    }
}