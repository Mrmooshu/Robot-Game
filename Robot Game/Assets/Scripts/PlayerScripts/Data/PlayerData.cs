using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : UnitData
{
    public string lastSafePoint;
    [SerializeReference] public Item weapon;
    [SerializeReference] public Item pickaxe;
    [SerializeReference] public Item hatchet;
    [SerializeReference] public Item fishingRod;

    public PlayerData(string player, Vector2 position)
    {
        CreateSkillTree(player);
        inventory = new ItemInventory(20, 10);
        this.savedPosition = position;
    }


    public override UnitEntity GetEntity()
    {
        foreach (PlayerEntity player in PlayerManager.instance.playerEntities)
        {
            if (player.data == this)
            {
                return player;
            }
        }
        return null;
    }
}
