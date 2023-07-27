using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourInverter : BehaviourNode
{
    public BehaviourInverter(List<BehaviourNode> children) : base(children) { }
    public override NodeState Evaluate()
    {
        switch (children[0].Evaluate())
        {
            case NodeState.FAILURE:
                state = NodeState.SUCCESS;
                return state;
            case NodeState.SUCCESS:
                state = NodeState.FAILURE;
                return state;
            case NodeState.RUNNING:
                state = NodeState.RUNNING;
                return state;
            default:
                state = NodeState.SUCCESS;
                return state;
        }
    }
}
