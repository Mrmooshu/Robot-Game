using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().currentInventory.Remove(eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>().inventoryIndex);
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<InventoryDisplay>().RefreshInventory();
        }
    }
}
