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
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        wander.Wander(this);
    }

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

}
