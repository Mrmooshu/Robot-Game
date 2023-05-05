using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPassive : PassiveStat
{
    public override void InitializePassive(Entity host)
    {
        entity = (MinionEntity)host;
        modifier = new StatMod(((MinionEntity)entity).data.skills[abilityName] * 10, StatModType.Base, EntityStatType.Health);
        entity.stats[EntityStatType.Health].AddModifier(modifier);
    }

    public override void Refresh()
    {
        modifier.Value = ((MinionEntity)entity).data.skills[abilityName] * 10;
    }
}
