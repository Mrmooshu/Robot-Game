using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public override void PassInput(InputAction.CallbackContext context)
    {
        StopMining();
        base.PassInput(context);
    }

    public void IncrementCharge()
    {
        if (((ClayGolemData)data).ChargeLevel < 2)
        {
            ((ClayGolemData)data).ChargeLevel++;
        }
    }

    public virtual void BasicAttack()
    {
        //If first punch is in action then buffer a second punch
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Basic_Punch_First"))
        {
            AttemptAction(() => { animator.Play("Basic_Punch_Second", 0); });
        }
        else
        {
            AttemptAction(() => { animator.Play("Basic_Punch_First", 0); });
        }
    }

    // functions for all unique golem attacks

    public virtual void BasicAttackHit()
    {
        bufferedAction = null;
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

    public void Tornado(ActiveAbilityIcon active)
    {
        //If Tornado is charging
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Tornado_Charge"))
        {
            animator.Play("Tornado_Start", 0);
            StartCoroutine(TornadoMovement());
            StartActiveCooldown(active);
        }
        else
        {
            AttemptAction(() => { animator.Play("Tornado_Charge", 0); });
        }

        IEnumerator TornadoMovement()
        {
            animator.ResetTrigger("EndTornado");
            var charge = ((ClayGolemData)data).ChargeLevel;
            ((ClayGolemData)data).ChargeLevel = 0;
            var v = rigBod.velocity;
            v.y *= .3f;
            rigBod.velocity = v;
            ConstantForce2D force = gameObject.AddComponent<ConstantForce2D>();
            force.relativeForce = new Vector2(((3 * (charge + 1)) + (stats[EntityStatType.MoveSpeed].Value + 1)) * facingDirection * 3, 10.1f);
            var counter = charge * .4f + .8f;
            yield return new WaitForSeconds(.01f);
            while ((animator.GetCurrentAnimatorStateInfo(0).IsName("Tornado_Start") || animator.GetCurrentAnimatorStateInfo(0).IsName("Tornado_Active")) && counter > 0)
            {
                yield return new WaitForSeconds(.01f);
                counter -= .01f;
            }
            Destroy(force);
            animator.SetTrigger("EndTornado");
        }

    }


    public void TornadoHit()
    {
        bufferedAction = null;
        Vector2 knockback = new Vector2(100, 100); // replace this with a magnetic effect later that applies to the target and pulls them close for the following hits
        DamageScript.Attack(new DamageScript.attackData(this, new DamageScript.damageData(stats[EntityStatType.AttackDamage].Value * .1f, DamageScript.damageType.physical), true, knockback, .1f), hitboxes, whatIsEnemy, this);
    }
}
