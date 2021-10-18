using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public List<Item> currentInventory;

    public void SetInventory(List<Item> inventory)
    {
        currentInventory = inventory;
    }
}
