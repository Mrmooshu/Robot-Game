using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MinionData : UnitData
{
    public string variantName;
    public Item tool;

    public MinionData(string variantName)
    {
        this.variantName = variantName;
        CreateSkillTree(variantName);
        inventory = new ItemInventory(5, 10);
    }

    public override UnitEntity GetEntity()
    {
        foreach (MinionEntity minion in PlayerManager.instance.minionEntities)
        {
            if (minion.data == this)
            {
                return minion;
            }
        }
        return null;
    }
}
