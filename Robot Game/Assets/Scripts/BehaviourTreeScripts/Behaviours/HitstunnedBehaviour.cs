using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitstunnedBehaviour : BehaviourNode
{
    CharacterEntity host;

    public HitstunnedBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {
        if (host.animator.GetFloat("Hitstun") <= 0)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
