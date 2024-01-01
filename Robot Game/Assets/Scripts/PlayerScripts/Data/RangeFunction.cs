using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunction : ClassFunction
{
    public RangeFunction(MinionData host) : base(host)
    {
        itemType = typeof(RangeWeapon);
        name = "Range";
        if (host.style == MinionData.CombatStyle.None)
        {
            host.style = MinionData.CombatStyle.Range;
        }
    }
}
