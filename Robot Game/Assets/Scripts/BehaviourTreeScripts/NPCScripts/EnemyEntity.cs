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
    public Transform hitboxes;

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    protected override void CreateBrain()
    {
        brain = new BehaviourSequence(new List<BehaviourNode>
        {
            new IsInIdleBehaviour(this),
            new HitstunnedBehaviour(this),
            new TargetPlayerBehaviour(this, 5),
            new FaceTargetBehaviour(this),
            new ActionCooldownBehaviour(this, 2, "primaryActions"),
            new RandomBehaviour(new List<BehaviourNode>
            {
                new BehaviourSequence(new List<BehaviourNode>
                {
                    new LineSightTargetBehaviour(this, 5),
                    new SlimeSlideBehaviour(this, 20, 5)
                }),

                new BehaviourSequence(new List<BehaviourNode>
                {
                    new RandomJumpBehaviour(this),
                    new JumpBehaviour(this, 2)
                }),
                new BehaviourSequence(new List<BehaviourNode>
                {
                    new BehaviourInverter(new List<BehaviourNode> {new IsIinPursuitBehaviour(this) }),
                    new TurnAroundBehaviour(this)
                })
            })
        });
        brain.SetData("JumpDisabled", false);
        brain.SetData("Target", null);
        brain.SetData("SlideDisabled", false);
        brain.SetData("primaryActions", false);
        base.CreateBrain();
    }


}
