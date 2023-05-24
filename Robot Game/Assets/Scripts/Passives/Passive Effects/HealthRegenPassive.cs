using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenPassive : Passive
{
    private IEnumerator coroutine;

    public HealthRegenPassive(Entity host)
    {
        type = passiveType.single;
        entity = host;
        entities = new List<Entity> { host };
        coroutine = HealTick();
        AddEntity(host);
    }

    public override void Refresh()
    {
    }

    private IEnumerator HealTick()
    {
        while (entities[0] != null)
        {
            ((ResourceStat)(entities[0]).stats[EntityStatType.Health]).CurrentValue += (entities[0]).stats[EntityStatType.HealthRegen].Value * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void AddEntity(Entity entity)
    {
        entities[0].StartCoroutine(coroutine);
    }

    public override void RemoveEntity(Entity entity)
    {
        entities[0].StopCoroutine(coroutine);
    }
}
