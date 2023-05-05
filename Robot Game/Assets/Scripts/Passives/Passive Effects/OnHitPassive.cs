using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPassive : Passive, IOnHit
{
    public override void InitializePassive(Entity host)
    {
        entity = (MinionEntity)host;
        if (host is MinionEntity)
        {
            ((MinionEntity)host).onHitPassives.Add(this);
        }
    }

    public override void Refresh()
    {
    }

    public DamageScript.damageData OnHit(Entity target)
    {
        return new DamageScript.damageData(((MinionEntity)entity).data.skills[abilityName] * entity.stats[EntityStatType.Health].Value * .01f, DamageScript.damageType.magic);
    }
}
