using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenPassive : Passive
{
    public HealthRegenPassive(Entity host)
    {
        entity = host;
        entity.StartCoroutine(HealTick());
    }

    public override void Refresh()
    {
    }

    private IEnumerator HealTick()
    {
        while (entity != null)
        {
            ((ResourceStat)(entity).stats[EntityStatType.Health]).CurrentValue += (entity).stats[EntityStatType.HealthRegen].Value * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
