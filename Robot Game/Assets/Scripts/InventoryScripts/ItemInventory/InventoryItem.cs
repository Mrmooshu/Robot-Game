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
        if (transform.parent.parent.parent.parent.GetComponent<ItemInventoryDisplay>())
        {
            transform.parent.SetAsLastSibling();
            transform.parent.parent.parent.parent.SetAsLastSibling();
        }
        else if (transform.parent.GetComponent<SlotDisplay<Item>>())
        {
            transform.parent.parent.SetAsLastSibling();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
