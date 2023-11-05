using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSentinelData : MinionData
{
    protected override void Create()
    {
        base.Create();
        functions[0] = new WoodcuttingFunction(this);
        variantName = "Wood Sentinel";
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

    public override void InitializePassives()
    {
        var host = GetEntity();
        passives = new List<Passive>();
        passives.Add(new HealthRegenPassive(host));
        passives.Add(new PassiveStat("health passive", host, StatModType.Base, EntityStatType.Health, 5));
        passives.Add(new PassiveStat("mana passive", host, StatModType.Base, EntityStatType.Mana, 5));
    }

    new public WoodSentinelEntity GetEntity()
    {
        return (WoodSentinelEntity)base.GetEntity();
    }
}
