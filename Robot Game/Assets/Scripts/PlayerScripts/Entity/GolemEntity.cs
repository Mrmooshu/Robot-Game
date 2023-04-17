using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEntity : MinionEntity
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Mine"))
        {
            if (data.tool != null)
            {
                if (Database.GetItem(data.tool.itemID) is Pickaxe)
                {
                    if (data.tool != null)
                    {
                        //tool.UpdateAnimators();
                        rigBod.velocity = Vector2.zero;
                        animator.SetBool("Mining", true);
                        //tool.toolAnimator.SetBool("Skilling", true);
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
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Mine"))
        {
            animator.SetBool("Mining", false);
            //tool.toolAnimator.SetBool("Skilling", false);
        }

    }

    public override void ToolAction()
    {
        if (currentInteractable is RockInteractable)
        {
           ((RockInteractable) currentInteractable).RollDrop();
        }
    }
}
