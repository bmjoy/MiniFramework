using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    /// <summary>
    /// 队列：控制所有节点的执行
    /// </summary>
    public class Sequence : IPoolable, ISequence
    {
        public MonoBehaviour Executer { get; set; }
        public bool IsRecycled { get; set; }
        private List<IEnumerator> nodes = new List<IEnumerator>();
        public Sequence() { }
        public void Append(float seconds)
        {
            nodes.Add(delayCoroutine(seconds));
        }
        public void Append(Action action)
        {
            nodes.Add(actionCoroutine(action));
        }
        public void Append(Func<bool> condition)
        {
            nodes.Add(conditionCoroutine(condition));
        }

        public void Execute()
        {
            Executer.StartCoroutine(SequenceCoroutine());
        }
        private IEnumerator SequenceCoroutine()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                yield return Executer.StartCoroutine(nodes[i]);
            }
            Pool<Sequence>.Instance.Recycle(this);
        }
        private IEnumerator delayCoroutine(float time)
        {
            float cur = 0;
            while ((cur += Time.deltaTime) < time)
            {
                yield return null;
            }
        }
        private IEnumerator actionCoroutine(Action action)
        {
            action();
            yield return null;
        }
        private IEnumerator conditionCoroutine(Func<bool> condition)
        {
            while (!condition())
            {
                yield return null;
            }
        }
        public void OnRecycled()
        {
            nodes.Clear();
        }
    }
}