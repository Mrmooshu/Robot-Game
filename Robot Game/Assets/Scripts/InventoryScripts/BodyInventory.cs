using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class BodyInventory : BaseInventory<PlayerBody>
{
    public BodyInventory(int inventorySize) : base(inventorySize) { }

    public void Reinitialize(int inventorySize, int stackLimit)
    {
        this.inventorySize = inventorySize;
        Array.Resize(ref inventory, inventorySize);
    }

    public override bool Add(PlayerBody body)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == default)
            {
                inventory[i] = body;
                OnInventoryUpdated();
                return true;
            }
        }
        return false;
    }
}
