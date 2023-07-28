using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehaviour : BehaviourNode
{
    public RandomBehaviour(List<BehaviourNode> children) : base(children) { }

    public override NodeState Evaluate()
    {
        var choiceIndexes = new List<int>();
        for (int i = 0; i < children.Count; i++)
        {
            choiceIndexes.Add(i);
        }

        var result = NodeState.FAILURE;
        while (result == NodeState.FAILURE && choiceIndexes.Count > 0)
        {
            var selected = choiceIndexes[Random.Range(0, choiceIndexes.Count)];
            choiceIndexes.Remove(selected);
            result = children[selected].Evaluate();
        }
        return result;
    }
}
