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
        if (!host.canJump || !host.grounded || (bool)GetRoot().GetData("JumpDisabled"))
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
        yield return new WaitForSeconds(.1f);
        while (!host.grounded)
        {
            yield return new WaitForSeconds(.01f);
        }
        host.movementDirection = 0;
    }
}
