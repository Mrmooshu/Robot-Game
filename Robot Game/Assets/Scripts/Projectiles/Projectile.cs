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
    protected DamageScript.attackData attack;

    protected List<Entity> alreadyHit;

    public void Initialize(int facingDirection, Entity origin, DamageScript.attackData attack)
    {
        transform.right *= facingDirection;
        this.origin = origin;
        this.attack = attack;
        alreadyHit = new List<Entity>();
        CreateStats();
        StartCoroutine(Lifespan());
    }

    public override void Update()
    {
        // homing
        //direction.right = origin.transform.position - transform.position;
        //visual.right = origin.transform.position - transform.position;

        transform.position += direction.right * stats[EntityStatType.MoveSpeed].Value * Time.deltaTime;

        // despawn if far enough away from origin
        if (Vector2.Distance(origin.transform.position, transform.position) > 100)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null && UniversalHelperFunctions.LayerMaskCompare(origin.whatIsEnemy, collision) && !alreadyHit.Contains(collision.GetComponent<Entity>()))
        {
            DamageScript.Attack(attack, collision.GetComponent<Entity>(), origin);
            alreadyHit.Add(collision.GetComponent<Entity>());
            Die();
        }

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
        stats[EntityStatType.MoveSpeed].BaseValue = 0;
        visual.GetComponentsInChildren<Collider>().ToList().ForEach(x => x.enabled = false);
        animator.SetTrigger("Die");
    }
}
