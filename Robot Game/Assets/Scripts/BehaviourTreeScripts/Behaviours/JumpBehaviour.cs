using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourSystem;

public class JumpBehaviour : BehaviourNode
{
    CharacterEntity host;

    float forwardCheck;
    float upwardCheck;

    public JumpBehaviour(CharacterEntity host, float forward = 1f, float up = 1f)
    {
        this.host = host;
        forwardCheck = forward;
        upwardCheck = up;
    }

    public override NodeState Evaluate()
    {
        if (GetRoot().GetData("Jumping") == null && host.canJump && host.hitStunDuration <= 0)
        {
            if(Physics2D.OverlapPoint(host.groundCheck.transform.position + new Vector3(forwardCheck*host.facingDirection, .2f), host.whatIsGround) != null && Physics2D.OverlapPoint(host.groundCheck.transform.position + new Vector3(forwardCheck*host.facingDirection, upwardCheck), host.whatIsGround) == null)
            {
                GetRoot().SetData("Jumping", true);
                host.Invoke("JumpForward",0);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }


        state = NodeState.SUCCESS;
        return state;
    }
}
