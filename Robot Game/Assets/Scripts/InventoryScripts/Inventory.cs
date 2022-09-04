using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Item[] inventory;
    public int inventorySize;
    public int stackLimit;

    public Inventory(int inventorySize , int stackLimit)
    {
        this.inventorySize = inventorySize;
        this.stackLimit = stackLimit;

        inventory = new Item[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            inventory[i] = null;
        }
    }

    public void Reinitialize(int inventorySize, int stackLimit)
    {
        this.inventorySize = inventorySize;
        this.stackLimit = stackLimit;

        Array.Resize<Item>(ref inventory, inventorySize);
    }

    public bool Add(Item item)
    {
        int firstNullIndex = inventory.Length;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && Database.GetItem(item.itemID).stackable)
            {
                if (inventory[i].itemID == item.itemID && inventory[i].quanity < stackLimit)
                {
                    inventory[i].quanity += item.quanity;
                    if (inventory[i].quanity > stackLimit)
                    {
                        item.quanity = inventory[i].quanity - stackLimit;
                        inventory[i].quanity = stackLimit;
                        Add(item);
                    }
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
            return true;
        }
        return false;
    }

    public void Remove(int index)
    {
        inventory[index] = null;
    }

    public void Move(int index1, int index2)
    {
        if (index1 != index2)
        {
            Item temp = inventory[index2];
            inventory[index2] = inventory[index1];
            inventory[index1] = temp;
        }
    }

    public Item GetItem(int index)
    {
        return inventory[index];
    }

    public int GetSize()
    {
        return inventory.Length;
    }
}
