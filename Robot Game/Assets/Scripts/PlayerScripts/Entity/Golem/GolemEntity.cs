using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DamageScript;

public class GolemEntity : MinionEntity
{
    new public ClayGolemData data { get { return (ClayGolemData)base.data; } private set { base.data = value; } }

    [SerializeField] private GameObject sandblastPrefab;
    [SerializeField] private GameObject rangeBasicPrefab;
    [SerializeField] private Transform projectileSpawn;

    public void IncrementCharge()
    {
        if (data.ChargeLevel < 2)
        {
            data.ChargeLevel++;
        }
    }

    public override void MeleeBasic()
    {
        //If second punch is in action then buffer a third punch
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Melee_Basic_Second"))
        {
            AttemptAction(() => { animator.Play("Melee_Basic_Third", 0); });
        }
        //If first punch is in action then buffer a second punch
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Melee_Basic_First"))
        {
            AttemptAction(() => { animator.Play("Melee_Basic_Second", 0); });
        }
        else
        {
            AttemptAction(() => { animator.Play("Melee_Basic_First", 0); });
        }
    }

    public override void RangeBasic()
    {
        //If first punch is in action then buffer a second punch
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Range_Basic_First"))
        {
            AttemptAction(() => { animator.Play("Range_Basic_Second", 0); });
        }
        else
        {
            AttemptAction(() => { animator.Play("Range_Basic_First", 0); });
        }
    }

    public override void MagicBasic()
    {
        throw new System.NotImplementedException();
    }

    // functions for all unique golem attacks

    public virtual void BasicAttackHit()
    {
        bufferedAction = null;
        Vector2 knockback = new Vector2(30 * facingDirection,0);
        Vector2 airknockback = new Vector2(50 * facingDirection, 50);
        var attack = hitboxes.EnableAttack(new AttackData(this, new damageData(stats[EntityStatType.AttackDamage].Value, damageType.physical), true, (knockback, airknockback), .5f, whatIsEnemy));
        attack.AddAction((() => { SandblastAttack(attack); }, AttackData.effectOccurance.end));
        hitboxes.BeginAttack();

        void SandblastAttack(AttackData attack)
        {
            if (!attack.hit && data.skills["sandblast"] > 0)
            {
                Projectile projectile = Instantiate(sandblastPrefab, projectileSpawn.position, transform.rotation).GetComponent<Projectile>();
                projectile.Initialize(facingDirection, this, new AttackData(this, new damageData(4 * data.skills["sandblast"] + stats[EntityStatType.AttackDamage].Value * .1f + stats[EntityStatType.MagicDamage].Value, damageType.magic), false, (Vector2.zero, Vector2.zero), .5f, whatIsEnemy));
                // (4 per level + 10% ad + 100% ap) magic damage
            }
        }

        //rigBod.AddForce(new Vector2(stats[StatType.MoveSpeed].Value * facingDirection, 1.2f), ForceMode2D.Impulse);
    }

    public virtual void BasicRangeHit(float side)
    {
        Projectile projectile = Instantiate(rangeBasicPrefab, projectileSpawn.position, transform.rotation).GetComponent<Projectile>();
        projectile.Initialize(facingDirection, this, new AttackData(this, new damageData(10, damageType.magic), false, (Vector2.zero, Vector2.zero), .5f, whatIsEnemy));
        // add damage formula here
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
            var charge = data.ChargeLevel;
            data.ChargeLevel = 0;
            var v = rigBod.velocity;
            v.y *= .1f;
            rigBod.velocity = v;
            ConstantForce2D force = gameObject.AddComponent<ConstantForce2D>();
            force.relativeForce = new Vector2(((2 * (charge + 1)) + (stats[EntityStatType.MoveSpeed].Value + 1)) * facingDirection * 3, canJump? 0 : 15f);
            var counter = charge * .4f + .6f;
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
        Vector2 knockback = new Vector2(facingDirection * (rigBod.velocity.x + 20), 40); // replace this with a magnetic effect later that applies to the target and pulls them close for the following hits
        hitboxes.EnableAttack(new AttackData(this, new damageData(stats[EntityStatType.AttackDamage].Value * .1f, damageType.physical), true, (knockback, knockback), .5f, whatIsEnemy));
        hitboxes.BeginAttack();
    }
}
