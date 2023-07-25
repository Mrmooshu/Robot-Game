using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourSystem;

public class JumpBehaviour : BehaviourNode
{
    CharacterEntity host;

    float forwardCheck;
    float upwardCheck;

    public JumpBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {
        if (!host.canJump && host.hitStunDuration > 0)
        {
            state = NodeState.FAILURE;
            return state;
        }


        if (GetRoot().GetData("Jumping") == null)
        {
            GetRoot().SetData("Jumping", true);
            host.Invoke("JumpForward", 0);
            state = NodeState.SUCCESS;
            return state;
        }


        state = NodeState.RUNNING;
        return state;
    }
}
