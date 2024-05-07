using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunction : ClassFunction
{
    public override Type itemType { get { return typeof(RangeWeapon); } }

    protected override List<(EntityStatType, int)> uniquestats { get { return new List<(EntityStatType, int)> { (EntityStatType.rangepower, 1) }; } }

    public RangeFunction(MinionData host) : base(host,"Range")
    {
        if (host.style == MinionData.CombatStyle.None)
        {
            host.style = MinionData.CombatStyle.Range;
        }
    }
}
