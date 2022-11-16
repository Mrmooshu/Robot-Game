using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Base, Additive, Multiplicative, Flat
}

[Serializable]
public class StatMod
{
    public float value;
    public StatModType bonusType;
    public StatType statType;
    public object source;

    public StatMod(float value, StatModType bonusType, StatType statType, object source)
    {
        this.value = value;
        this.bonusType = bonusType;
        this.statType = statType;
        this.source = source;
    }

    public StatMod(float value, StatModType bonusType, StatType statType) : this(value, bonusType, statType, null) { }
}
