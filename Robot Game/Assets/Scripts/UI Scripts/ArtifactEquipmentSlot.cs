using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ArtifactEquipmentSlot : SlotDisplay
{
    public int artifactSlotIndex;

    public override ref Item GetItem()
    {
        return ref PlayerManager.instance.activeMinion.artifacts[artifactSlotIndex];
    }

    public override void Swap(Transform inventorySlot, bool condition)
    {
        if (inventorySlot.GetComponentInChildren<InventoryItem>())
        {
            base.Swap(inventorySlot, Database.GetItem(inventorySlot.GetComponentInChildren<InventoryItem>().item.itemID).GetType().IsSubclassOf(typeof(Artifact)));
        }
        else
        {
            base.Swap(inventorySlot, condition);
        }
    }
}
