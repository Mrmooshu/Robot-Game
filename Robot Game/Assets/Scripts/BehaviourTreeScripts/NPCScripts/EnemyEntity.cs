using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourSystem;

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
            new JumpBehaviour(this),
            new WanderBehaviour(this)
        });
        base.CreateBrain();
    }

    public override void JumpForward()
    {
        rigBod.AddForce(new Vector2(stats[EntityStatType.JumpForce].Value*facingDirection/2, stats[EntityStatType.JumpForce].Value), ForceMode2D.Impulse);
        //animator.SetBool("Jumping", true);
        StartCoroutine(JumpCooldown());

    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(1);
        brain.ClearData("Jumping");
    }

}
