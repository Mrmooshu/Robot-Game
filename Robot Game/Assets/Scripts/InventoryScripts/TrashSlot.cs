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
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory.Remove(eventData.pointerDrag.transform.parent.GetComponent<ItemInventorySlot>().inventoryIndex);
            eventData.pointerDrag.transform.parent.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
        }
    }
}
