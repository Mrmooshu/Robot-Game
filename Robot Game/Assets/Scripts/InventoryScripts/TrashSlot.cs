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
            transform.parent.GetComponent<InventoryDisplay>().currentInventory.Remove(eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>().inventoryIndex);
            Destroy(eventData.pointerDrag);
        }
    }
}
