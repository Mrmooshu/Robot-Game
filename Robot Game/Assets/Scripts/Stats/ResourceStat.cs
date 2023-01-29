using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStat : Stat
{
    private float currentValue;
    public float CurrentValue { get { return currentValue; } set { currentValue = value > 0 ? value : 0; currentValueUpdated?.Invoke(); } }

    public event Action currentValueUpdated;

    public ResourceStat(float baseValue) : base(baseValue)
    {
        CurrentValue = baseValue;
    }


}
