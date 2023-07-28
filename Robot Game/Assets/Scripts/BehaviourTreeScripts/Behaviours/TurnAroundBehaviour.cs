using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundBehaviour : BehaviourNode
{
    CharacterEntity host;

    public TurnAroundBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {
        host.SetDirection(Random.Range(0, 2) == 0 ? 1 : -1);
        state = NodeState.SUCCESS;
        return state;
    }
}


