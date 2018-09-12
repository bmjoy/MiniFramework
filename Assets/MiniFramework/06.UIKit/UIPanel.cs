using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniFramework
{
    public abstract class UIPanel : MonoBehaviour,IBeginDragHandler,IDragHandler
    {
        public Action OpenAnimation;
        public Action CloseAnimation;
        public virtual void Open()
        {
            if (OpenAnimation != null)
            {
                OpenAnimation();
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        public virtual void Close()
        {
            if (CloseAnimation != null)
            {
                CloseAnimation();
            }
            else
            {
                gameObject.SetActive(false);
            }
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