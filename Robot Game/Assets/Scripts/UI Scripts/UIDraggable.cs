using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIDraggable : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public CanvasGroup canvasGroup;
    public Canvas canvas;
    public bool draggable = true;
    public virtual void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.position = eventData.position;
            canvasGroup.alpha = .5f;
            canvasGroup.blocksRaycasts = false;
            canvas.sortingOrder = 15;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.localPosition = Vector2.zero;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvas.sortingOrder = 14;
        }
    }

    public abstract void OnPointerDown(PointerEventData eventData);
}
