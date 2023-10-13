using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class WeaponInventorySlot : SlotDisplay
{
    public override ref Item GetItem()
    {
        return ref PlayerManager.instance.activeMinion.weapon;
    }

    public override void Swap(Transform inventorySlot, bool condition)
    {
        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            base.Swap(inventorySlot, Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID).GetType().IsSubclassOf(typeof(Weapon)));
        }
        else
        {
            base.Swap(inventorySlot, condition);
        }
    }
}
