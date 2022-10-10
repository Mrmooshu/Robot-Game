using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInventorySlot : MonoBehaviour, IDropHandler, ISlot
{
    public int inventoryIndex;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.GetComponentInParent<ISlot>().Swap(transform);
        }
    }

    public void Swap(Transform inventorySlot)
    {
        ItemInventory.Move(inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, transform.parent.parent.GetComponentInParent<ItemInventoryDisplay>().currentInventory, inventorySlot.GetComponent<ItemInventorySlot>().inventoryIndex, inventoryIndex);
        inventorySlot.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
        transform.parent.parent.GetComponentInParent<ItemInventoryDisplay>().RefreshInventory();
    }
}
