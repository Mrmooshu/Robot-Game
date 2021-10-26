using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryDisplay inventoryDisplay;
    public Inventory inventory;
    public int inventoryIndex;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            inventory.Move(eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>().inventoryIndex, inventoryIndex);
            inventoryDisplay.RefreshInventory();
        }
    }
}
