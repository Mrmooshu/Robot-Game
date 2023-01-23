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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            if (core.currentBody.tool != null)
            {
                if (Database.GetItem(core.currentBody.tool.itemID) is Pickaxe)
                {
                    if (core.currentBody.tool != null)
                    {
                        tool.UpdateAnimators();
                        rigBod.velocity = Vector2.zero;
                        animator.SetBool("Skilling", true);
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
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetBool("Skilling", false);
            tool.toolAnimator.SetBool("Skilling", false);
        }

    }

    public override void ToolAction()
    {
        if (currentInteractable is RockInteractable)
        {
           ((RockInteractable) currentInteractable).RollDrop();
        }
    }

    public override void BasicAttack()
    {
        DamageScript.Attack(new DamageScript.attackData(this, new DamageScript.damageData(stats[StatType.AttackDamage].Value, DamageScript.damageType.physical), true), hitboxes, whatIsEnemy, this);
        rigBod.AddForce(new Vector2(stats[StatType.MoveSpeed].Value * facingDirection, 1.2f), ForceMode2D.Impulse);
        //test, delete this later plox
        DamageScript.ApplyDamage(this, new DamageScript.attackData(this, new DamageScript.damageData(10, DamageScript.damageType.physical), false));
    }
}
