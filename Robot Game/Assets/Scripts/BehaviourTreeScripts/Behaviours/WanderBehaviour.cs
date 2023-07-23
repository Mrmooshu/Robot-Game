using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourSystem;

public class WanderBehaviour : BehaviourNode
{
    CharacterEntity host;

    private float waitTime = 5f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public WanderBehaviour(CharacterEntity host)
    {
        this.host = host;
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                waiting = false;
            }
        }
        else
        {
            waiting = true;
            waitCounter = 0;
            switch (Random.Range(0, 3))
            {
                case 0:
                    StartWalking(Random.Range(2f, 4f));
                    break;
                case 1:
                    StartWaiting(Random.Range(.5f, 1f));
                    break;
                case 2:
                    host.Flip();
                    StartWaiting(Random.Range(.1f, .5f));
                    break;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

    protected void StartWalking(float duration)
    {
        waitTime = duration;
        host.movementDirection = host.facingDirection;
    }

    protected virtual void StartWaiting(float duration)
    {
        waitTime = duration;
        host.movementDirection = 0;
    }
}
