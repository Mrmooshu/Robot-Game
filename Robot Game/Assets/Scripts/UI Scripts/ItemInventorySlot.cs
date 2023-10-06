using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInventorySlot : SlotDisplay<ItemData>
{
    public int inventoryIndex;

    public ItemInventory inventory;

    public override ref Item GetItem()
    {
        return ref inventory.inventory[inventoryIndex];
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.GetComponentInParent<ISlot>().Swap(transform);
        }
    }

}
