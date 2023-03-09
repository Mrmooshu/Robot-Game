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
    public StatType statType;
}

[Serializable]
public class StatMod
{
    private float _value;
    public float Value { get { return _value; } set { _value = value; if(stat != null)stat.Recalculate(); } } 
    public StatModType bonusType;
    public StatType statType;
    public object source;
    public Stat stat;

    public StatMod(float value, StatModType bonusType, StatType statType, object source)
    {
        Value = value;
        this.bonusType = bonusType;
        this.statType = statType;
        this.source = source;
    }

    public StatMod(float value, StatModType bonusType, StatType statType) : this(value, bonusType, statType, null) { }
}
