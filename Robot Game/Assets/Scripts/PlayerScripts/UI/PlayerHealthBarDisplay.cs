using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarDisplay : PercentDisplayBar
{
    public override void Start()
    {
        base.Start();
        Inititalize(() => ((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.health]).CurrentValue, () => ((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.health]).Value);
    }
}
