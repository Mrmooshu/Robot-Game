using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerBehaviour : BehaviourNode
{
    CharacterEntity host;

    float range;

    public TargetPlayerBehaviour(CharacterEntity host, float range)
    {
        this.host = host;
        this.range = range;
    }

    public override NodeState Evaluate()
    {
        if (GetRoot().GetData("Target") == null)
        {
            var hit = Physics2D.OverlapCircle(host.transform.position, range, host.whatIsEnemy);
            if (hit != null)
            {
                GetRoot().SetData("Target", hit.gameObject);
            }
        }
        else
        {
            // remove target if they are outside of double the agro range
            if (Vector2.Distance(((GameObject)GetRoot().GetData("Target")).transform.position, host.transform.position) > range*2)
            {
                GetRoot().SetData("Target", null);
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
