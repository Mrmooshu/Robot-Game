using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPlayer : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private CanvasGroup canvasGroup;
    public MinionData unit;
    public bool draggable;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.position = eventData.position;
            canvasGroup.alpha = .5f;
            canvasGroup.blocksRaycasts = false;
            GetComponent<Canvas>().sortingOrder = 15;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            transform.localPosition = Vector2.zero;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            GetComponent<Canvas>().sortingOrder = 14;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SafePointEntity.selectedMinion = unit;
    }
}
