using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPassive : PassiveStat
{
    public override void InitializePassive(Entity host)
    {
        entity = (PlayerEntity)host;
        modifier = new StatMod(entity.core.currentBody.skills[abilityName] * 10, StatModType.Base, StatType.Health);
        entity.stats[StatType.Health].AddModifier(modifier);
    }

    public override void Refresh()
    {
        modifier.Value = entity.core.currentBody.skills[abilityName] * 10;
    }
}
