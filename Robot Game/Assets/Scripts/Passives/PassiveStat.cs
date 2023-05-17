using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStat : Passive
{
    protected StatMod modifier;

    public PassiveStat(string name, Entity host, StatModType mathType, EntityStatType statType)
    {
        skillName = name;
        entity = host;
        modifier = new StatMod(StatFormula(), mathType, statType);
        entity.stats[statType].AddModifier(modifier);
    }

    public override void Refresh()
    {
        modifier.Value = StatFormula();
    }

    protected float StatFormula()
    {
        return ((MinionEntity)entity).data.skills[skillName];
    }
}
