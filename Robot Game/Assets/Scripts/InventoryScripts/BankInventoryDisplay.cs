using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankInventoryDisplay : InventoryDisplay
{

    public override void RefreshInventory()
    {
        currentInventory = PlayerManager.instance.bankInventory;
        CreateInventory();
    }

}
