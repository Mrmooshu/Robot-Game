using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : CharacterEntity
{
    public enum Aggresion
    {
        passive, neutral, aggresive
    }

    public Aggresion aggresion;
    public WanderBehaviour wander;

    public override void Start()
    {
        base.Start();
        wander = new WanderBehaviour(this);
        CreateStats(new List<(StatType, float)> {
            (StatType.Health, 100),
            (StatType.AttackDefense, 20),
            (StatType.MagicDefense, 20),
            (StatType.MoveSpeed, 2),
            (StatType.JumpForce, 2),
            (StatType.Gravity, 2)
        });
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        wander.Wander(this);
    }

}
