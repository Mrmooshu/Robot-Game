using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public PlayerEntity player;
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
        int firstNullIndex = inventory.Length;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].data.itemName == item.data.itemName && inventory[i].quanity < player.stackSizeLimit)
                {
                    inventory[i].quanity += item.quanity;
                    if (inventory[i].quanity > player.stackSizeLimit)
                    {
                        item.quanity = inventory[i].quanity - player.stackSizeLimit;
                        inventory[i].quanity = player.stackSizeLimit;
                        Add(item);
                    }
                    return true;
                }
            }
            else if (i < firstNullIndex)
            {
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
