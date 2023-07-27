using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomJumpBehaviour : BehaviourNode
{
    CharacterEntity host;

    public RandomJumpBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {

        if (host.canJump && !(bool)GetRoot().GetData("JumpDisabled"))
        {
            if(GetRoot().GetData("Target") != null)
            {
                host.SetDirection(((Collider2D)GetRoot().GetData("Target")).gameObject.transform.position.x > host.transform.position.x? 1:-1);
            }
            else
            {
                host.SetDirection(Random.Range(0, 2) == 0 ? 1 : -1);
            }
            float jumpPower = Random.Range(host.stats[EntityStatType.JumpForce].Value * .5f, host.stats[EntityStatType.JumpForce].Value);
            GetRoot().SetData("JumpArc", new Vector2(host.facingDirection * jumpPower / 2, jumpPower));
            state = NodeState.SUCCESS;
            return state;
        }


        state = NodeState.FAILURE;
        return state;
    }
}
