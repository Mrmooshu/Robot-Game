using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStat : Stat
{
    public float currentValue;

    public ResourceStat(float baseValue) : base(baseValue)
    {
        currentValue = baseValue;
    }


}
