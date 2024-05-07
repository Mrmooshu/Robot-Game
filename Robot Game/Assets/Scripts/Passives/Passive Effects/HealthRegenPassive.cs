using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenPassive : Passive
{
    private IEnumerator coroutine;

    public HealthRegenPassive(MinionEntity host = null)
    {
        coroutine = HealTick();
        if (host != null)
        {
            ChangeEntity(host);
        }
    }

    private IEnumerator HealTick()
    {
        while (host != null)
        {
            ((ResourceStat)host.stats[EntityStatType.health]).CurrentValue += host.stats[EntityStatType.healthregen].Value * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void ChangeEntity(MinionEntity entity)
    {
        base.ChangeEntity(entity);
        entity.StartCoroutine(coroutine);
    }

    public override void RemoveEntity()
    {
        host.StopCoroutine(coroutine);
        base.RemoveEntity();
    }
}
