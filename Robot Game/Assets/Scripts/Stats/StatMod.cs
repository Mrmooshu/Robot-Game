using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Base, Additive, Multiplicative, Flat
}

[Serializable]
public struct ModData
{
    public float value;
    public StatModType bonusType;
    public EntityStatType statType;
}

[Serializable]
public class StatMod
{
    public delegate float StatFormula();

    private StatFormula _value;
    public StatFormula Value { get { return _value; } set { _value = value; if(stat != null)stat.Recalculate(); } } 
    public StatModType bonusType;
    public EntityStatType statType;
    public object source;
    public Stat stat;

    public StatMod(StatFormula value, StatModType bonusType, EntityStatType statType, object source)
    {
        Value = value;
        this.bonusType = bonusType;
        this.statType = statType;
        this.source = source;
    }

    public StatMod(StatFormula value, StatModType bonusType, EntityStatType statType) : this(value, bonusType, statType, null) { }

    public StatMod(float value, StatModType bonusType, EntityStatType statType, object source) : this(() => value, bonusType, statType, source) { }

    public StatMod(float value, StatModType bonusType, EntityStatType statType) : this(() => value, bonusType, statType, null) { }

}
