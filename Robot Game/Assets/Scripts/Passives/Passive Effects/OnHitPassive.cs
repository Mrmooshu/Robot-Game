using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPassive : Passive, IOnHit
{
    public override void InitializePassive(Entity host)
    {
        entity = (PlayerEntity)host;
        if (host is PlayerEntity)
        {
            ((PlayerEntity)host).onHitPassives.Add(this) ;
        }
    }

    public override void Refresh()
    {
    }

    public DamageScript.damageData OnHit(Entity target)
    {
        return new DamageScript.damageData(entity.core.currentBody.skills[abilityName] * entity.stats[StatType.Health].Value * .01f, DamageScript.damageType.magic);
    }
}
