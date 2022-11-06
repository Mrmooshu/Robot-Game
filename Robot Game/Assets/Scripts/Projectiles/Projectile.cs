using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    public Entity origin;
    public Transform direction;
    public Transform visual;
    public float speed;

    public void Initialize(float speed, int facingDirection, Entity origin)
    {
        this.speed = speed;
        transform.right *= facingDirection;
        this.origin = origin;
        direction = transform.Find("Direction");
        visual = transform.Find("Visual");
        CreateStats(new List<(StatType, float)> { (StatType.MoveSpeed, speed)});
    }

    public override void Update()
    {
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
