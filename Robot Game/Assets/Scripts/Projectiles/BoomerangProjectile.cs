using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectile
{
    public float duration = 1;
    public float rotationSpeed = 450f;

    public override void Update()
    {
        base.Update();
        duration -= Time.deltaTime;

        // homing
        if (duration <= 0)
        {
            direction.right = origin.transform.position - transform.position;
        }

        // movement
        transform.position += direction.right * (speed + (Mathf.Abs(duration)*10)) * Time.deltaTime;

        // rotate boomerang
        visual.Rotate(0,0,-rotationSpeed * Time.deltaTime);

        // despawn if far enough away from origin
        if (Vector2.Distance(origin.transform.position, transform.position) > 100 || Vector2.Distance(origin.transform.position, transform.position) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
