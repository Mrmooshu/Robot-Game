using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPassive : PassiveStat
{
    public override void InitializePassive(Entity host)
    {
        entity = (MinionEntity)host;
        modifier = new StatMod(((MinionEntity)entity).data.skills[abilityName] * 10, StatModType.Base, EntityStatType.Mana);
        entity.stats[EntityStatType.Mana].AddModifier(modifier);
    }

    public override void Refresh()
    {
        modifier.Value = ((MinionEntity)entity).data.skills[abilityName] * 10;
    }
}
