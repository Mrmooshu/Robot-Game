using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCore
{
    public PlayerBody currentBody;
    public ItemInventory inventory;

    public PlayerCore(PlayerBody currentBody, int inventorySize, int stackLimit)
    {
        this.currentBody = currentBody;
        inventory = new ItemInventory(inventorySize, stackLimit);
    }

    public PlayerEntity GetPlayer()
    {
        foreach(PlayerEntity player in PlayerManager.instance.players)
        {
            if (player.core == this)
            {
                return player;
            }
        }
        return null;
    }
}
