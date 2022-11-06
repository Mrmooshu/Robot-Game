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
    public readonly float Value;
    public readonly StatModType Type;
    public readonly object Source;

    public StatMod(float value, StatModType type, object source)
    {
        Value = value;
        Type = type;
        Source = source;
    }

    public StatMod(float value, StatModType type) : this(value, type, null) { }
}
