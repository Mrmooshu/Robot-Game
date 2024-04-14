using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Projectile : Entity
{
    public Entity origin;
    [SerializeField] protected float lifeTime = 20;
    [SerializeField] protected Transform direction;
    [SerializeField] protected Transform visual;
    protected AttackData attack;

    public void Initialize(int facingDirection, Entity origin, AttackData attack)
    {
        transform.right *= facingDirection;
        this.origin = origin;
        this.attack = attack;
        whatIsEnemy = origin.whatIsEnemy;
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CreateStats();
        StartCoroutine(Lifespan());
    }

    public override void Start()
    {

        hitboxes.EnableAttack(attack);
        attack.AddAction((() => { Die(); }, AttackData.effectOccurance.hit));
        hitboxes.BeginAttack();
    }

    public override void Update()
    {
        // homing
        //direction.right = origin.transform.position - transform.position;
        //visual.right = origin.transform.position - transform.position;

        transform.position += direction.right * stats[EntityStatType.movespeed].Value * Time.deltaTime;

        // despawn if far enough away from origin
        if (Vector2.Distance(origin.transform.position, transform.position) > 100)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Die();
        }
    }

    protected IEnumerator Lifespan()
    {
        // eternal projectile if lifespawn lower than 0
        if (lifeTime < 0)
        {
            yield break;
        }
        yield return new WaitForSeconds(lifeTime);
        Die();
    }

    protected override void Die()
    {
        base.Die();
        stats[EntityStatType.movespeed].BaseValue = 0;
        visual.GetComponentsInChildren<Collider>().ToList().ForEach(x => x.enabled = false);
        animator.SetTrigger("Die");
    }
}
