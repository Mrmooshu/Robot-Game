using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    public GameObject slotPrefab;
    protected GameObject objectPrefab;
    public GameObject inventoryArea;
    public int columns = 5;
    public int slotSize = 34;

    public virtual void RefreshInventory()
    {
        UpdateCurrentInventory();
        CreateInventory();
    }

    protected abstract void CreateInventory();

    public abstract void UpdateCurrentInventory();
}
