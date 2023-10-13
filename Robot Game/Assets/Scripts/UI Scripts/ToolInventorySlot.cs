using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ToolInventorySlot : SlotDisplay
{
    public override ref Item GetItem()
    {
        return ref PlayerManager.instance.activeMinion.tool;
    }

    public override void Swap(Transform inventorySlot, bool condition)
    {
        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            base.Swap(inventorySlot, Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID).GetType().IsSubclassOf(typeof(Tool)));
        }
        else
        {
            base.Swap(inventorySlot, condition);
        }
    }
}
