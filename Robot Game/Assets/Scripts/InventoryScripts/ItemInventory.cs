using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInventory : BaseInventory<Item>
{
    public int stackLimit = 0;

    public static event Action nullDefaultItems;

    public ItemInventory(int inventorySize , int stackLimit) : base(inventorySize)
    {
        this.stackLimit = stackLimit;
        nullDefaultItems += NullDefaultItems;
    }

    public ItemInventory(int inventorySize) : base(inventorySize) { }

    public void Reinitialize(int inventorySize, int stackLimit)
    {
        this.inventorySize = inventorySize;
        this.stackLimit = stackLimit;
        Array.Resize(ref inventory, inventorySize);
    }

    public override bool Add(Item item)
    {
        int firstNullIndex = inventory.Length;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && Database.GetItem(item.itemID).stackable)
            {
                if (inventory[i].itemID == item.itemID && (inventory[i].quanity < stackLimit || stackLimit == 0))
                {
                    inventory[i].quanity += item.quanity;
                    if (inventory[i].quanity > stackLimit && stackLimit != 0)
                    {
                        item.quanity = inventory[i].quanity - stackLimit;
                        inventory[i].quanity = stackLimit;
                        Add(item);
                    }
                    OnInventoryUpdated();
                    return true;
                }
            }
            else if (inventory[i] == null && i < firstNullIndex)
            {
                if (!Database.GetItem(item.itemID).stackable)
                {
                    inventory[i] = item;
                }
                firstNullIndex = i;
            }
        }
        if (firstNullIndex != inventory.Length)
        {
            inventory[firstNullIndex] = item;
            OnInventoryUpdated();
            return true;
        }
        return false;
    }

    private void NullDefaultItems()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].itemID == 0)
                {
                    inventory[i] = null;
                }
            }
        }
    }

    public static void CallNullDefaultItems()
    {
        nullDefaultItems?.Invoke();
    }
}
