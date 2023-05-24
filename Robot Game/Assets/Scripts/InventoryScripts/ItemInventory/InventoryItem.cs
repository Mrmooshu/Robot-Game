using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    public Item item;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
        GetComponent<Canvas>().sortingOrder = 14;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        GetComponent<Canvas>().sortingOrder = 13;
    }
}
