using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPassive : PassiveStat
{
    public override void InitializePassive(Entity host)
    {
        entity = (PlayerEntity)host;
        modifier = new StatMod(entity.core.currentBody.skills[abilityName] * 10, StatModType.Base, StatType.Mana);
        entity.stats[StatType.Mana].AddModifier(modifier);
    }

    public override void Refresh()
    {
        modifier.Value = entity.core.currentBody.skills[abilityName] * 10;
    }
}
