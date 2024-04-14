using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageScript;

public class JumpBehaviour : BehaviourNode
{
    CharacterEntity host;

    float cooldown;

    public JumpBehaviour(CharacterEntity host, float cooldown)
    {
        this.host = host;
        this.cooldown = cooldown;
    }

    public override NodeState Evaluate()
    {
        if (!host.canJump || !host.animator.GetBool("Grounded") || (bool)GetRoot().GetData("JumpDisabled"))
        {
            state = NodeState.FAILURE;
            return state;
        }

        Vector2 vec = (Vector2)GetRoot().GetData("JumpArc");
        host.canJump = false;
        host.rigBod.AddForce(vec, ForceMode2D.Impulse);
        //animator.SetBool("Jumping", true);
        GetRoot().ClearData("JumpArc");
        host.StartCoroutine(JumpCooldown());
        JumpMovement();
        state = NodeState.SUCCESS;
        return state;
    }

    private IEnumerator JumpCooldown()
    {
        GetRoot().SetData("JumpDisabled", true);
        yield return new WaitForSeconds(cooldown);
        GetRoot().SetData("JumpDisabled", false);
    }

    private void JumpMovement()
    {
        host.movementDirection = host.facingDirection;
        Vector2 knockback = new Vector2(host.facingDirection * 10, 10);
        var attack = host.hitboxes.EnableAttack(new AttackData(host, new damageData(host.stats[EntityStatType.damagepower].Value, damageType.melee), true, (knockback, knockback), .5f, host.whatIsEnemy));
        attack.AddAction((() => { host.movementDirection = 0; }, AttackData.effectOccurance.end));
        host.hitboxes.BeginAttack();
    }
}
