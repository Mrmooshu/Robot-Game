using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class BaseInventory<T>
{
    [SerializeReference] public T[] inventory;
    public int inventorySize;
    public int currentPage = 1;

    public static event Action inventoryUpdated;

    public BaseInventory(int inventorySize)
    {
        this.inventorySize = inventorySize;

        inventory = new T[inventorySize];
    }

    public void Reinitialize(int inventorySize)
    {
        this.inventorySize = inventorySize;
        Array.Resize(ref inventory, inventorySize);
    }

    public abstract bool Add(T thing);

    public void Remove(int index)
    {
        inventory[index] = default;
        inventoryUpdated?.Invoke();
    }

    public static void Move(BaseInventory<T> inventoryFrom, BaseInventory<T> inventoryTo, int indexFrom, int indexTo)
    {
        if (inventoryFrom == inventoryTo && indexFrom == indexTo)
        {
            return;
        }
        T temp = inventoryTo.inventory[indexTo];
        inventoryTo.inventory[indexTo] = inventoryFrom.inventory[indexFrom];
        inventoryFrom.inventory[indexFrom] = temp;
        inventoryUpdated?.Invoke();
    }

    public static void Move(BaseInventory<T> inventory, ref T individual, int index)
    {
        T temp = inventory.inventory[index];
        inventory.inventory[index] = individual;
        individual = temp;
        inventoryUpdated?.Invoke();
    }

    public T GetSlotByIndex(int index)
    {
        return inventory[index];
    }

    public int GetSize()
    {
        return inventory.Length;
    }

    protected void OnInventoryUpdated()
    {
        inventoryUpdated?.Invoke();
    }
}
