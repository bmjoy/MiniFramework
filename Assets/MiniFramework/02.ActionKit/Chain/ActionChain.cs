using UnityEngine;
using System;
namespace MiniFramework
{
    /// <summary>
    /// 链式调用类
    /// </summary>
    public static class ActionChain
    {
        public static void Delay<T>(this T selfBehaviour,float seconds,Action action) where T : MonoBehaviour
        {
            Sequence sequence = Pool<Sequence>.Instance.Allocate();
            sequence.Executer = selfBehaviour;
            sequence.Delay(seconds);
            sequence.Event(action);
            sequence.Execute();
        }
        public static Sequence Sequence<T>(this T selfBehaviour) where T : MonoBehaviour
        {
            Sequence sequence = Pool<Sequence>.Instance.Allocate();
            sequence.Executer = selfBehaviour;
            return sequence;
        }
        //internal static Sequence Delay<T>(this T sequence, float seconds) where T:IDelay
        //{
        //    sequence.Delay(seconds);
        //    return sequence;
        //}
        //internal static Sequence Event(this Sequence sequence, Action action)
        //{
        //    sequence.Event(action);
        //    return sequence;
        //}
        //internal static Sequence Until(this Sequence sequence, Func<bool> condition)
        //{
        //    sequence.Until(condition);
        //    return sequence;
        //}
        //internal static void Start(this Sequence sequence)
        //{
        //    sequence.Execute();
        //}
    }
}