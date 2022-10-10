using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class BaseInventory<T>
{
    public T[] inventory;
    public int inventorySize;
    public int currentPage = 1;

    public static event Action inventoryUpdated;

    public BaseInventory(int inventorySize)
    {
        this.inventorySize = inventorySize;

        inventory = new T[inventorySize];
        //for (int i = 0; i < inventorySize; i++)
        //{
            //inventory[i] = default;
        //}
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

    public static void Move(BaseInventory<T> inventoryFrom, BaseInventory<T> inventoryTo, int index1, int index2)
    {
        if (inventoryFrom == inventoryTo && index1 == index2)
        {
            return;
        }
        T temp = inventoryTo.inventory[index2];
        inventoryTo.inventory[index2] = inventoryFrom.inventory[index1];
        inventoryFrom.inventory[index1] = temp;
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
