using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Item[] inventory;
    public int inventorySize;
    public int stackLimit = 0;
    public int currentPage = 1;

    public static event Action inventoryUpdated;

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

    public Inventory(int inventorySize)
    {
        this.inventorySize = inventorySize;

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

    public void Reinitialize(int inventorySize)
    {
        this.inventorySize = inventorySize;
        Array.Resize<Item>(ref inventory, inventorySize);
    }

    public bool Add(Item item)
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
                    inventoryUpdated?.Invoke();
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
            inventoryUpdated?.Invoke();
            return true;
        }
        return false;
    }

    public void Remove(int index)
    {
        inventory[index] = null;
        inventoryUpdated?.Invoke();
    }

    public static void Move(Inventory inventoryFrom, Inventory inventoryTo, int index1, int index2)
    {
        if (inventoryFrom == inventoryTo && index1 == index2)
        {
            return;
        }
        Item temp = inventoryTo.inventory[index2];
        inventoryTo.inventory[index2] = inventoryFrom.inventory[index1];
        inventoryFrom.inventory[index1] = temp;
        inventoryUpdated?.Invoke();
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
