using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPassive1 : PassiveStat
{
    public override void InitializePassive(Entity target)
    {
        entity = (PlayerEntity)target;
        modifier = new StatMod(entity.core.currentBody.skills[abilityName] * 10, StatModType.Base, StatType.Health);
        entity.stats[StatType.Health].AddModifier(modifier);
    }

    public override void Refresh()
    {
        modifier.Value = entity.core.currentBody.skills[abilityName] * 10;
    }
}
