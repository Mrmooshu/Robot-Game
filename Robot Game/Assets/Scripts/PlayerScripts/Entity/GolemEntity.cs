using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEntity : MinionEntity
{
    [SerializeField] private GameObject sandblastPrefab;
    [SerializeField] private Transform sandblastSpawn;

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

    public override void BasicAttack()
    {
        StopMining();
        if (data.weapon != null)
        {

        }
        else
        {
            //If first punch is in action buffer second punch
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Basic_Punch_First"))
            {
                bufferedAction = "Basic_Punch_Second";
            }
            //If second punch is in action buffer first punch
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Basic_Punch_Second"))
            {
                bufferedAction = "Basic_Punch_First";
            }
            //If there are no buffered actions
            if (bufferedAction == "")
            {
                animator.Play("Basic_Punch_First", 0);
            }
        }

    }

    protected override void DefaultBasicHit()
    {
        bufferedAction = "";
        Vector2 knockback = new Vector2(100 * facingDirection, 100);
        if (!DamageScript.Attack(new DamageScript.attackData(this, new DamageScript.damageData(stats[EntityStatType.AttackDamage].Value, DamageScript.damageType.physical), true, knockback, .5f), hitboxes, whatIsEnemy, this)){
            if (data.skills["sandblast"] > 0)
            {
                Projectile projectile = Instantiate(sandblastPrefab, sandblastSpawn.position, transform.rotation).GetComponent<Projectile>();
                projectile.Initialize(facingDirection, this, new DamageScript.attackData(this, new DamageScript.damageData(stats[EntityStatType.AttackDamage].Value * .1f + stats[EntityStatType.MagicDamage].Value, DamageScript.damageType.magic), false, Vector2.zero, .5f));
            }

        }
        //rigBod.AddForce(new Vector2(stats[StatType.MoveSpeed].Value * facingDirection, 1.2f), ForceMode2D.Impulse);
    }
}
