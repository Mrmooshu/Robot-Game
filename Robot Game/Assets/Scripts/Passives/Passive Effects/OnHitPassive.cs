using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPassive : Passive
{
    public override void InitializePassive(Entity host)
    {
        entity = (PlayerEntity)host;
        if (host is PlayerEntity)
        {
            ((PlayerEntity)host).onHitEvent += OnHit;
        }
    }

    public override void Refresh()
    {
    }

    private void OnHit(Entity target)
    {
        DamageScript.ApplyDamage(target, new DamageScript.attackData(entity, new DamageScript.damageData(entity.core.currentBody.skills[abilityName] * entity.stats[StatType.Health].Value * .01f, DamageScript.damageType.magic),false));
    }
}
