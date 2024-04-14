using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningFunction : ClassFunction
{
    public MiningFunction(MinionData host) : base(host,"Mining")
    {
        itemType = typeof(Pickaxe);
        uniquestats = new List<(EntityStatType, int)> { (EntityStatType.miningpower, 1) };
    }

    public override void InitializePassives()
    {
        var entity = host.GetEntity();
        host.passives.Add(new PassiveStat("miningskill 1", entity, StatModType.Base, EntityStatType.miningpower, 1f * (level.level + host.Level.level)));
        // 1 mining power per the sum of class level and mining level
    }

    public void StartMining()
    {
        var entity = host.GetEntity();

        if (equipItem != null)
        {
            if (Database.GetItem(equipItem.itemID) is Pickaxe)
            {
                entity.rigBod.velocity = Vector2.zero;
                entity.animator.Play("Mine",0);
                entity.weapon.sprite = Database.GetItem(equipItem.itemID).sprite;
                return;
            }
            else
            {
                Debug.Log("tool is not a pickaxe");
            }
        }
        else
        {
            Debug.Log("no tool equiped");
        }
    }
}
