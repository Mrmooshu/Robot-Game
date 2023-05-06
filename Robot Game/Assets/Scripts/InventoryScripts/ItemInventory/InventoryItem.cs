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
        if (GetComponentInParent<ItemInventoryDisplay>())
        {
            GetComponentInParent<ItemInventorySlot>().transform.SetAsLastSibling();
            GetComponentInParent<ItemInventoryDisplay>().transform.SetAsLastSibling();
        }
        else if (GetComponentInParent<SlotDisplay<Item>>())
        {
            GetComponentInParent<SlotDisplay<Item>>().transform.parent.SetAsLastSibling();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
