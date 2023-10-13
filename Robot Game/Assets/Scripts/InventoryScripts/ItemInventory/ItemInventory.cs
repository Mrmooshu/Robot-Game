using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInventory : BaseInventory<Item>
{
    public int stackLimit = 0;

    public ItemInventory(int inventorySize , int stackLimit) : base(inventorySize)
    {
        this.stackLimit = stackLimit;
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

    public static void Move(ItemInventory inventory, ref Item individual, int index)
    {
        if (individual == null || inventory.inventory[index] == null)
        {
            BaseInventory<Item>.Move(inventory, ref individual, index);
            return;
        }
        else if (inventory.inventory[index].itemID == individual.itemID)
        {
            var value = inventory.inventory[index].quanity + individual.quanity;
            if (value > inventory.stackLimit)
            {
                inventory.inventory[index].quanity = inventory.stackLimit;
                individual.quanity = value - inventory.stackLimit;
            }
            else
            {
                individual.quanity = value;
                inventory.inventory[index] = null;
            }
        }
        BaseInventory<Item>.Move(inventory, ref individual, index);
    }

    public static void MoveUpToLimit(ItemInventory inventory, ref Item itemTo, int index, int limit)
    {
        if (itemTo == null)
        {
            itemTo = new Item(inventory.inventory[index].itemID, 0);
        }

        var itemFrom = inventory.inventory[index];
        var total = itemTo.quanity + itemFrom.quanity;
        if (total > limit)
        {
            itemTo.quanity = limit;
            itemFrom.quanity = total - limit;
        }
        else
        {
            itemTo.quanity = total;
            inventory.inventory[index] = null;
        }
        inventory.OnInventoryUpdated();
    }
}
