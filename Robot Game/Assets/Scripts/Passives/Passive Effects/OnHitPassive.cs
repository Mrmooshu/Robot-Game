using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPassive : Passive
{
    public delegate DamageScript.damageData OnHitEffect(Entity target);

    protected OnHitEffect onHit;

    public OnHitPassive(string name, Entity host, OnHitEffect onHitEffect)
    {
        type = passiveType.single;
        skillName = name;
        entity = host;
        entities = new List<Entity> { };
        onHit = onHitEffect;
        if (host is MinionEntity)
        {
            AddEntity(host);
        }
    }

    public OnHitPassive(string name, List<Entity> hosts, OnHitEffect onHitEffect)
    {
        type = passiveType.multiple;
        skillName = name;
        entities = new List<Entity> { };
        onHit = onHitEffect;
        foreach (Entity host in hosts)
        {
            if (host is MinionEntity)
            {
                AddEntity(host);
            }
        }

    }

    public override void Refresh()
    {
    }

    public DamageScript.damageData OnHit(Entity target)
    {
        return onHit(target);
    }

    public override void AddEntity(Entity entity)
    {
        entities.Add(entity);
        if (entity is MinionEntity)
        {
            ((MinionEntity)entity).onHitPassives.Add(this);
        }

    }

    public override void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
        if (entity is MinionEntity)
        {
            ((MinionEntity)entity).onHitPassives.Remove(this);
        }
    }
}
