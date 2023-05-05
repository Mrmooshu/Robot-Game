using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    public GameObject slotPrefab;
    protected GameObject objectPrefab;
    public GameObject inventoryArea;
    public GameObject pageNumber;
    public int columns = 5;
    public int slotsPerPage = 25;

    public virtual void RefreshInventory()
    {
        UpdateCurrentInventory();
        CreateInventory();
    }

    protected abstract void CreateInventory();

    public abstract void UpdateCurrentInventory();

    // used by page select arrows
    public abstract void IncrementPage();

    // used by page select arrows
    public abstract void DecrementPage();
}
