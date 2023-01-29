using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCore : ISerializationCallbackReceiver
{
    public enum Activity
    {
        Idle, Mining, Woodcutting, Fishing
    }

    public PlayerBody currentBody;
    public ItemInventory inventory;
    public Vector3 position;
    public Activity activity = Activity.Idle;
    public string lastSafePoint;


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

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        try
        {
            position = GetPlayer().transform.position;
        }
        catch (Exception)
        {

        }
    }
}
