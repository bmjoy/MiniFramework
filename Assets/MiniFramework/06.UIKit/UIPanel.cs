using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniFramework
{
    public abstract class UIPanel : MonoBehaviour,IBeginDragHandler,IDragHandler
    {
        public UIPanelState State;
        public bool IsCanDrag;
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

        Vector3 offset;
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsCanDrag)
            {
                return;
            }
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out pos);
            offset = transform.position - pos;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!IsCanDrag)
            {
                return;
            }
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out pos);
            transform.position = pos + offset;
        }
    }
}