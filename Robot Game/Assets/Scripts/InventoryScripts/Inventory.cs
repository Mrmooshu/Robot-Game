using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Item[] inventory;

    public Inventory(int size)
    {
        inventory = new Item[size];
        for (int i = 0; i < size; i++)
        {
            inventory[i] = null;
        }
    }

    public bool Add(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return true;
            }
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
            inventory[index2] = inventory[index1];
            inventory[index1] = null;
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
