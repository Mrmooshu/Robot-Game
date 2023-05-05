using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEntitiy : HealthBar
{
    public Entity entity;

    public override void Start()
    {
        base.Start();
        if (entity == null)
        {
            entity = GetComponentInParent<Entity>();
        }
    }

    void Update()
    {
        healthBar.fillAmount = ((ResourceStat)entity.stats[EntityStatType.Health]).CurrentValue / ((ResourceStat)entity.stats[EntityStatType.Health]).Value;
        text.text = (int)((ResourceStat)entity.stats[EntityStatType.Health]).CurrentValue + "/" + (int)((ResourceStat)entity.stats[EntityStatType.Health]).Value;
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
