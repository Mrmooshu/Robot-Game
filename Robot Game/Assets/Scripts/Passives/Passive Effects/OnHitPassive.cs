using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPassive : Passive
{
    public delegate DamageScript.damageData OnHitEffect(Entity target);

    protected OnHitEffect onHit;

    public OnHitPassive(string name, OnHitEffect onHitEffect, MinionEntity host = null)
    {
        passiveName = name;
        onHit = onHitEffect;
        if (host != null)
        {
            ChangeEntity(host);
        }
    }

    public DamageScript.damageData OnHit(Entity target)
    {
        return onHit(target);
    }

    public override void ChangeEntity(MinionEntity entity)
    {
        base.ChangeEntity(entity);
        entity.onHitPassives.Add(this);

    }

    public override void RemoveEntity()
    {
        host.onHitPassives.Remove(this);
        base.RemoveEntity();
    }
}
