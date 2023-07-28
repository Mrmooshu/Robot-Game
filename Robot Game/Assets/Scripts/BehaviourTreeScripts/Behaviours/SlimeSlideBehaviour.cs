using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSlideBehaviour : BehaviourNode
{
    CharacterEntity host;

    float cooldown;
    float speed;

    public SlimeSlideBehaviour(CharacterEntity host, float speed, float cooldown)
    {
        this.host = host;
        this.cooldown = cooldown;
        this.speed = speed;
    }

    public override NodeState Evaluate()
    {
        if (host.animator.GetCurrentAnimatorStateInfo(0).IsName("Slide_Start") || host.animator.GetCurrentAnimatorStateInfo(0).IsName("Slide_Active") || host.animator.GetCurrentAnimatorStateInfo(0).IsName("Slide_End"))
        {
            state = NodeState.RUNNING;
            return state;
        }

        if ((bool)GetRoot().GetData("SlideDisabled") || GetRoot().GetData("Target") == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        host.SetDirection(((GameObject)GetRoot().GetData("Target")).transform.position.x > host.transform.position.x ? 1 : -1);
        host.StartCoroutine(SlideCooldown());
        host.StartCoroutine(SlideMovement());

        state = NodeState.SUCCESS;
        return state;
    }

    private IEnumerator SlideCooldown()
    {
        GetRoot().SetData("SlideDisabled", true);
        yield return new WaitForSeconds(cooldown);
        GetRoot().SetData("SlideDisabled", false);
    }

    private IEnumerator SlideMovement()
    {
        host.animator.ResetTrigger("EndSlide");
        host.animator.Play("Slide_Start", 0);
        host.animator.SetTrigger("EndSlide");
        ConstantForce2D force = host.gameObject.AddComponent<ConstantForce2D>();
        force.relativeForce = new Vector2(host.facingDirection * speed,-.1f);
        ((EnemyEntity)host).hitboxes.GetChild(0).gameObject.AddComponent<DamageCollider>().Initialize(new DamageScript.attackData(host, new DamageScript.damageData(host.stats[EntityStatType.AttackDamage].Value, DamageScript.damageType.physical), true, new Vector2(host.facingDirection * 10,10), .5f), host.whatIsEnemy, host);

        while (!host.animator.GetCurrentAnimatorStateInfo(0).IsName("Slide_End"))
        {
            yield return new WaitForSeconds(.01f);
        }
        Object.Destroy(force);
        Object.Destroy(((EnemyEntity)host).hitboxes.GetChild(0).gameObject.GetComponent<DamageCollider>());
    }
}