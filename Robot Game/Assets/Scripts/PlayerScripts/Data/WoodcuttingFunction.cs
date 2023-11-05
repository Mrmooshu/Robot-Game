using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcuttingFunction : ClassFunction
{
    public WoodcuttingFunction(MinionData host) : base(host)
    {
        itemType = typeof(Hatchet);
    }

    public void StartChopping()
    {
        var entity = host.GetEntity();

        if (equipItem != null)
        {
            if (Database.GetItem(equipItem.itemID) is Hatchet)
            {
                entity.rigBod.velocity = Vector2.zero;
                entity.animator.SetBool("Chopping", true);
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
