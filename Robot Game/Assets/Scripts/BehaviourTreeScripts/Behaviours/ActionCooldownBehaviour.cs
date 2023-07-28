using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCooldownBehaviour : BehaviourNode
{
    CharacterEntity host;

    float cooldown;

    string actionGroupName;

    public ActionCooldownBehaviour(CharacterEntity host, float cooldown, string actionGroupName)
    {
        this.host = host;
        this.cooldown = cooldown;
        this.actionGroupName = actionGroupName;
    }

    public override NodeState Evaluate()
    {
        if ((bool)GetRoot().GetData(actionGroupName) == false)
        {
            host.StartCoroutine(ActionCooldown());
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

    private IEnumerator ActionCooldown()
    {
        GetRoot().SetData(actionGroupName, true);
        yield return new WaitForSeconds(cooldown);
        GetRoot().SetData(actionGroupName, false);
    }
}