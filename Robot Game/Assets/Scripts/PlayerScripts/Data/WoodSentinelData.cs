using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSentinelData : MinionData
{
    public override GameObject Blueprint { get { return GeneralManager.instance.entityPrefabs.LoadAsset<GameObject>("Wood Sentinel"); } }

    public WoodSentinelData(MinionData data = null) : base(data)
    {

    }

    protected override void Create(MinionData data = null)
    {
        base.Create(data);
        functions[0] = new WoodcuttingFunction(this);
    }

    protected override void CreateSkills()
    {
        // create fresh skill tree values from variants skill tree
        skills = new Dictionary<string, int>()
        {
            { "health passive", 0},
            { "mana passive", 0}








        };
    }

    new public WoodSentinelEntity GetEntity()
    {
        return (WoodSentinelEntity)base.GetEntity();
    }
}
