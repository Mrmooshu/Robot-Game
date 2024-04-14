using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunction : ClassFunction
{
    public RangeFunction(MinionData host) : base(host,"Range")
    {
        itemType = typeof(RangeWeapon);
        if (host.style == MinionData.CombatStyle.None)
        {
            host.style = MinionData.CombatStyle.Range;
        }
        uniquestats = new List<(EntityStatType, int)> { (EntityStatType.rangepower, 1) };
    }
}
