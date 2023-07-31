using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        host.StartCoroutine(JumpMovement());
        state = NodeState.SUCCESS;
        return state;
    }

    private IEnumerator JumpCooldown()
    {
        GetRoot().SetData("JumpDisabled", true);
        yield return new WaitForSeconds(cooldown);
        GetRoot().SetData("JumpDisabled", false);
    }

    private IEnumerator JumpMovement()
    {
        host.movementDirection = host.facingDirection;
        Vector2 knockback = new Vector2(host.facingDirection * 10, 10);
        ((SlimeEnemy)host).hitboxes.GetChild(0).gameObject.AddComponent<DamageCollider>().Initialize(new DamageScript.attackData(host, new DamageScript.damageData(host.stats[EntityStatType.AttackDamage].Value, DamageScript.damageType.physical), true, (knockback, knockback), .5f), host.whatIsEnemy, host);
        yield return new WaitForSeconds(.1f);
        while (!host.animator.GetBool("Grounded"))
        {
            yield return new WaitForSeconds(.01f);
        }
        host.movementDirection = 0;
        Object.Destroy(((SlimeEnemy)host).hitboxes.GetChild(0).gameObject.GetComponent<DamageCollider>());
    }
}
