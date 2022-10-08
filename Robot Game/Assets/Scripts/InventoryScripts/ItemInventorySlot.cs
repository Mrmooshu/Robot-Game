using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInventorySlot : MonoBehaviour, IDropHandler
{
    public int inventoryIndex;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemInventory.Move(eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, transform.parent.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, eventData.pointerDrag.transform.parent.GetComponent<ItemInventorySlot>().inventoryIndex, inventoryIndex);
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
            transform.parent.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
        }
    }
}
