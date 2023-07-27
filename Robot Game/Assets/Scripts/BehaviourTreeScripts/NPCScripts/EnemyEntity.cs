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

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    protected override void CreateBrain()
    {
        brain = new BehaviourSelector(new List<BehaviourNode>
        {
            new BehaviourSequence(new List<BehaviourNode>
            {
                new TargetPlayerBehaviour(this, 5),
                new RandomJumpBehaviour(this),
                new JumpBehaviour(this, 2)
            }),
        });
        brain.SetData("JumpDisabled", false);
        brain.SetData("Target", null);
        base.CreateBrain();
    }
}
