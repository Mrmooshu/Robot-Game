using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEntitiy : PercentDisplayBar
{
    public Entity entity;

    public override void Start()
    {
        base.Start();
        if (entity == null)
        {
            entity = GetComponentInParent<Entity>();
            Inititalize(() => ((ResourceStat)entity.stats[EntityStatType.Health]).CurrentValue, () => ((ResourceStat)entity.stats[EntityStatType.Health]).Value);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (entity.facingDirection < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
