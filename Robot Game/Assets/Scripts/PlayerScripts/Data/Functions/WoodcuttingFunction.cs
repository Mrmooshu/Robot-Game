using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcuttingFunction : ClassFunction
{
    public override Type itemType { get { return typeof(Hatchet); } }

    protected override List<(EntityStatType, int)> uniquestats { get { return new List<(EntityStatType, int)> { (EntityStatType.woodcuttingpower, 1) }; } }
    public WoodcuttingFunction(MinionData host) : base(host,"Woodcutting")
    {
    }

    public void StartChopping()
    {
        var entity = host.GetEntity();

        if (equipItem != null)
        {
            if (Database.GetItem(equipItem.itemID) is Hatchet)
            {
                entity.rigBod.velocity = Vector2.zero;
                entity.animator.Play("Chop", 0);
                entity.weapon.sprite = Database.GetItem(equipItem.itemID).sprite;
                return;
            }
            else
            {
                Debug.Log("tool is not a hatchet");
            }
        }
        else
        {
            Debug.Log("no tool equiped");
        }
    }
}
