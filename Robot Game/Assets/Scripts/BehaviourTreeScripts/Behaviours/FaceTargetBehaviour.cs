using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTargetBehaviour : BehaviourNode
{
    CharacterEntity host;

    public FaceTargetBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {
        if (GetRoot().GetData("Target") != null)
        {
            host.SetDirection(((GameObject)GetRoot().GetData("Target")).transform.position.x > host.transform.position.x ? 1 : -1);
        }
        state = NodeState.SUCCESS;
        return state;
    }
}