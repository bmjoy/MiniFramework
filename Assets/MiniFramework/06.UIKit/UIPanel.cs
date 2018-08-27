using System;
using UnityEngine;
namespace MiniFramework
{
    public abstract class UIPanel:MonoBehaviour
    {
        public Action OpenAnimation;
        public Action CloseAnimation;
        public virtual void Open() {
            if (OpenAnimation != null)
            {
                OpenAnimation();
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        public virtual void Close() {
            if (CloseAnimation != null)
            {
                CloseAnimation();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        public abstract void SetOpenAnimation();
        public abstract void SetCloseAnimation();
    }
}