using System;
using UnityEngine;
namespace MiniFramework
{
    /// <summary>
    /// 添加至需缓存对象上
    /// </summary>
    public class NeedPoolObject : MonoBehaviour
    {
        public event Action RecycleCallback;
        public bool IsRecycled { get; set; }
        public void OnRecycled()
        {
            if (RecycleCallback != null)
            {
                RecycleCallback();
            }
        }
    }
}