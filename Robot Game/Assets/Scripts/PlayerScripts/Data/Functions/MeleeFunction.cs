using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunction : ClassFunction
{
    public MeleeFunction(MinionData host) : base(host,"Melee")
    {
        itemType = typeof(MeleeWeapon);
        if (host.style == MinionData.CombatStyle.None)
        {
            host.style = MinionData.CombatStyle.Melee;
        }
        uniquestats = new List<(EntityStatType, int)> { (EntityStatType.meleepower, 1) };
    }

    public override void InitializePassives()
    {
        var entity = host.GetEntity();
        host.passives.Add(new PassiveStat("meleeskill 1", entity, StatModType.Base, EntityStatType.meleepower, 1f * (level.level + host.Level.level)));
        // 1 melee power per the sum of class level and melee level
    }
}
