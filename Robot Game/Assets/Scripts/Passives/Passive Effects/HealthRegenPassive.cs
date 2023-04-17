using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenPassive : Passive
{
    public override void InitializePassive(Entity host)
    {
        entity = (MinionEntity)host;
        entity.StartCoroutine(HealTick());
    }

    public override void Refresh()
    {
    }

    private IEnumerator HealTick()
    {
        while (entity != null)
        {
            ((ResourceStat)(entity).stats[StatType.Health]).CurrentValue += (entity).stats[StatType.HealthRegen].Value * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
