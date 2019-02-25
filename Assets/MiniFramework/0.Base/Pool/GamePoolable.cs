using UnityEngine;
using UnityEngine.Events;

namespace MiniFramework
{
    /// <summary>
    /// 添加至需缓存对象上
    /// </summary>
    public class GamePoolable : MonoBehaviour
    {
        public bool IsRecycled { get; set; }
        public UnityEvent OnRecycled;
    }
}