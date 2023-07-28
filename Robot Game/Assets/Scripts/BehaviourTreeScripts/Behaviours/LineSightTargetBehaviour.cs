using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSightTargetBehaviour : BehaviourNode
{
    CharacterEntity host;
    float range;

    public LineSightTargetBehaviour(CharacterEntity host, float range)
    {
        this.host = host;
        this.range = range;
    }

    public override NodeState Evaluate()
    {
        if (Physics2D.Raycast(host.transform.position, new Vector2(host.facingDirection, 0), range, host.whatIsEnemy))
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}