using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningFunction : ClassFunction
{
    public MiningFunction(MinionData host) : base(host)
    {
        itemType = typeof(Pickaxe);
        name = "Mining";
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
