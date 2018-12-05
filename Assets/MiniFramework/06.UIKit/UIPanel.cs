using UnityEngine;
namespace MiniFramework
{
    public abstract class UIPanel : MonoBehaviour
    {
        public UIPanelState State;
        public abstract void Open(params object[] paramList);
        public abstract void Close(params object[] paramList);

        public virtual void OnEnable()
        {
            State = UIPanelState.Open;
        }
        public virtual void OnDisable()
        {
            State = UIPanelState.Close;
        }
        public void SetLayerToTop()
        {
            transform.SetAsLastSibling();
        }
        public void SetLayerToButtom()
        {
            transform.SetAsFirstSibling();
        }
    }
}