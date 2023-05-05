using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankInventoryDisplay : ItemInventoryDisplay
{
    public override void UpdateCurrentInventory()
    {
        currentInventory = PlayerManager.instance.bankInventory;
    }
}
