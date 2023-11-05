using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FunctionInventorySlot : SlotDisplay
{
    public int functionSlotIndex;

    public override ref Item GetItem()
    {
        return ref PlayerManager.instance.activeMinion.functions[functionSlotIndex].equipItem;
    }

    public override void Swap(Transform inventorySlot, bool condition)
    {
        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            base.Swap(inventorySlot, Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID).GetType().Equals(PlayerManager.instance.activeMinion.functions[functionSlotIndex].itemType));
        }
        else
        {
            base.Swap(inventorySlot, condition);
        }
    }
}
