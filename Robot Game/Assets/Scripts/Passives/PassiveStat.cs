using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStat : Passive
{
    private StatMod.StatFormula formula;

    protected StatMod modifier;

    private StatModType mathType;

    private EntityStatType statType;

    public PassiveStat(string name, StatModType mathType, EntityStatType statType, StatMod.StatFormula formula, MinionEntity host = null)
    {
        passiveName = name.ToLower();
        this.mathType = mathType;
        this.statType = statType;
        this.formula = formula;
        modifier = new StatMod(formula, mathType, statType);
        if (host != null)
        {
            ChangeEntity(host);
        }
    }

    public override void ChangeEntity(MinionEntity entity)
    {
        base.ChangeEntity(entity);
        host.stats[statType].AddModifier(modifier);
    }

    public override void RemoveEntity()
    {
        host.stats[statType].RemoveModifier(modifier);
        base.RemoveEntity();
    }
}
