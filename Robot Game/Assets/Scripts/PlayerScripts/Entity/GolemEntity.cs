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
            StartMining();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Mine"))
        {
            StopMining();
        }
    }

    private void StartMining()
    {
        if (data.tool != null)
        {
            if (Database.GetItem(data.tool.itemID) is Pickaxe)
            {
                rigBod.velocity = Vector2.zero;
                animator.SetBool("Mining", true);
                return;
            }
            else
            {
                Debug.Log("tool is not a pickaxe");
            }
        }
        else
        {
            Debug.Log("no tool equiped");
        }
    }

    private void StopMining()
    {
        animator.SetBool("Mining", false);
    }

    public override void ToolAction()
    {
        if (currentInteractable is RockInteractable)
        {
           ((RockInteractable) currentInteractable).RollDrop();
        }
    }

    protected override void Movement()
    {
        base.Movement();
        if (rigBod.velocity.x > 0.1f)
        {
            StopMining();
        }
    }

    protected override void DefaultBasic()
    {
        Vector2 knockback = new Vector2(100 * facingDirection, 100);
        DamageScript.Attack(new DamageScript.attackData(this, new DamageScript.damageData(stats[StatType.AttackDamage].Value, DamageScript.damageType.physical), true, knockback, .5f), hitboxes, whatIsEnemy, this);
    }
}
