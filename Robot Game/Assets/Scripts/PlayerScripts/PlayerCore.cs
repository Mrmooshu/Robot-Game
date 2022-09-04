using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore
{
    public PlayerBody currentBody;
    public Inventory inventory;
    public int inventorySize;
    public int stackLimit;
    public GameObject bodyObject;
    public PlayerCore(PlayerBody currentBody, int inventorySize, int stackLimit)
    {
        this.currentBody = currentBody;
        this.inventorySize = inventorySize;
        this.stackLimit = stackLimit;
        inventory = new Inventory(inventorySize, stackLimit);
    }

}
