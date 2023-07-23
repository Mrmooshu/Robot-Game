using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourSystem;

public class ActionBehaviour : BehaviourNode
{
    CharacterEntity host;
    string actionName;

    public ActionBehaviour(CharacterEntity host, string actionName)
    {
        this.host = host;
        this.actionName = actionName;
    }

    public override NodeState Evaluate()
    {

        state = NodeState.SUCCESS;
        return state;
    }
}
