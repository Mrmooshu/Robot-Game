using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaTitanData : MinionData
{
    protected override void Create()
    {
        base.Create();
        functions[0] = new SmithingFunction(this);
        variantName = "Magma Titan";
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
        passives.Add(new PassiveStat("health passive", host, StatModType.Base, EntityStatType.health, 5));
        passives.Add(new PassiveStat("mana passive", host, StatModType.Base, EntityStatType.mana, 5));
    }

    new public MagmaTitanEntity GetEntity()
    {
        return (MagmaTitanEntity)base.GetEntity();
    }
}
