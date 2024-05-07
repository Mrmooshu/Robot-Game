using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunction : ClassFunction
{
    public override Type itemType { get { return typeof(MeleeWeapon); } }

    protected override List<(EntityStatType, int)> uniquestats { get { return new List<(EntityStatType, int)> { (EntityStatType.meleepower, 1) }; } }

    public MeleeFunction(MinionData host) : base(host,"Melee")
    {
        if (host.style == MinionData.CombatStyle.None)
        {
            host.style = MinionData.CombatStyle.Melee;
        }
    }

    public override void InitializePassives()
    {
        var entity = host.GetEntity();
        host.GetEntity().passives.Add(new PassiveStat("meleeskill 1", StatModType.Base, EntityStatType.meleepower, () => 1f * (level.Level + host.Level.Level), host : entity));
        // 1 melee power per the sum of class level and melee level
    }
}
