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
            sequence.Append(seconds);
            sequence.Append(action);
            sequence.Execute();
        }
        public static ISequence Sequence<T>(this T selfBehaviour) where T : MonoBehaviour
        {
            Sequence sequence = Pool<Sequence>.Instance.Allocate();
            sequence.Executer = selfBehaviour;
            return sequence;
        }
        internal static ISequence Delay(this ISequence sequence, float seconds)
        {
            sequence.Append(seconds);
            return sequence;
        }
        internal static ISequence Event(this ISequence sequence, Action action)
        {
            sequence.Append(action);
            return sequence;
        }
        internal static ISequence Until(this ISequence sequence, Func<bool> condition)
        {
            sequence.Append(condition);
            return sequence;
        }
        internal static ISequence Start(this ISequence sequence)
        {
            sequence.Execute();
            return sequence;
        }
    }
}