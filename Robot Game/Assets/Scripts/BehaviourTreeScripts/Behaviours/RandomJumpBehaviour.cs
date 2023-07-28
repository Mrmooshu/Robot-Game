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
            float jumpPower = Random.Range(host.stats[EntityStatType.JumpForce].Value * .5f, host.stats[EntityStatType.JumpForce].Value);
            GetRoot().SetData("JumpArc", new Vector2(host.facingDirection * jumpPower / 2, jumpPower));
            state = NodeState.SUCCESS;
            return state;
        }


        state = NodeState.FAILURE;
        return state;
    }
}
