using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsIinPursuitBehaviour : BehaviourNode
{
    CharacterEntity host;

    public IsIinPursuitBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {
        if (GetRoot().GetData("Target") == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}

