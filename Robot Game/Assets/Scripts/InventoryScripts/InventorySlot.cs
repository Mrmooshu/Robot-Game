using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryDisplay inventoryDisplay;
    public ItemInventory inventory;
    public int inventoryIndex;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemInventory.Move(eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().currentInventory, transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().currentInventory, eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>().inventoryIndex, inventoryIndex);
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().RefreshInventory();
            transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().RefreshInventory();
        }
    }
}
