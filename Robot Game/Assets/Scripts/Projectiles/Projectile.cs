using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    public Entity origin;
    public Transform direction;
    public Transform visual;

    protected List<Entity> alreadyHit;

    public void Initialize(int facingDirection, Entity origin)
    {
        transform.right *= facingDirection;
        this.origin = origin;
        direction = transform.Find("Direction");
        visual = transform.Find("Visual");
        alreadyHit = new List<Entity>();
        CreateStats();
    }

    public override void Update()
    {
        base.Update();
        // homing
        //direction.right = origin.transform.position - transform.position;
        //visual.right = origin.transform.position - transform.position;

        transform.position += direction.right * stats[StatType.MoveSpeed].Value * Time.deltaTime;

        // despawn if far enough away from origin
        if (Vector2.Distance(origin.transform.position, transform.position) > 100)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
