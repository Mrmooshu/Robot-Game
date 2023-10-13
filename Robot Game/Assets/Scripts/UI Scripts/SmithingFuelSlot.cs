using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class SmithingFuelSlot : SlotDisplay, IDropHandler, ISlot
{
    public override void RemoveFromSlot()
    {
        PlayerManager.instance.smithing.stations[0].furnaceFuel = null;
    }

    public override ref Item GetItem()
    {
        return ref PlayerManager.instance.smithing.stations[0].furnaceFuel;
    }

    public override void Swap(Transform inventorySlot, bool condition)
    {
        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            base.Swap(inventorySlot, Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID).tags.Contains(ItemData.ItemTags.Fuel));
        }
        else
        {
            base.Swap(inventorySlot, condition);
        }
    }
}
