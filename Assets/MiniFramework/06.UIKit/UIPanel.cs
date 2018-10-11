using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniFramework
{
    public enum UIPanelState
    {
        Close,
        Open,
        Playing,
    }
    public abstract class UIPanel : MonoBehaviour,IBeginDragHandler,IDragHandler
    {
        public UIPanelState State;
        public bool IsCanDrag;
        public abstract void Open();
        public abstract void Close();
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