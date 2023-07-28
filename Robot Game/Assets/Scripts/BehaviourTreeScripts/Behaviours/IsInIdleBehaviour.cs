using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInIdleBehaviour : BehaviourNode
{
    CharacterEntity host;

    public IsInIdleBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {
        if (host.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
