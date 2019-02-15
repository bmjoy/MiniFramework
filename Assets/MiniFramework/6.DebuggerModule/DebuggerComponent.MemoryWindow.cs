using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    public partial class DebuggerComponent
    {

        private  class MemoryWindow<T> : IDebuggerWindow where T : UnityEngine.Object
        {

            public void Initialize(params object[] args)
            {
                //throw new System.NotImplementedException();

            }
            public void OnDraw()
            {
                //throw new System.NotImplementedException();
            }

            public void OnUpdate(float time, float realTime)
            {
                //throw new System.NotImplementedException();
            }
            public void Close()
            {
                //throw new System.NotImplementedException();
            }

            private void TakeSample()
            {
                T[] samples = Resources.FindObjectsOfTypeAll<T>();
            }
        }
    }
}