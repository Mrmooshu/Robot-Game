using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    public class BehaviourSelector : BehaviourNode
    {
        public BehaviourSelector() : base() { }
        public BehaviourSelector(List<BehaviourNode> children) : base(children) { }
        public override NodeState Evaluate()
        {
            foreach (BehaviourNode node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}

