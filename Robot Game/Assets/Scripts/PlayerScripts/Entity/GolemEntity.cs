using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEntity : PlayerEntity
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void ToggleMining()
    {
        if (!upperAnimator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            if (core.currentBody.tool != null)
            {
                if (Database.GetItem(core.currentBody.tool.itemID) is Pickaxe)
                {
                    if (core.currentBody.tool != null)
                    {
                        tool.UpdateAnimators();
                        rigBod.velocity = Vector2.zero;
                        tool.toolAnimator.SetBool("Skilling", true);
                        return;
                    }
                    Debug.Log("need to equip a pick to mine");
                }
                else
                {
                    Debug.Log("tool is not a tool");
                }
            }
            else
            {
                Debug.Log("no tool equiped");
            }
        }
        else if (upperAnimator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            tool.toolAnimator.SetBool("Skilling", false);
        }

    }

    public override void ToolAction()
    {
        if (currentInteractable is RockEntity)
        {
           ((RockEntity) currentInteractable).RollDrop();
        }
    }
}
