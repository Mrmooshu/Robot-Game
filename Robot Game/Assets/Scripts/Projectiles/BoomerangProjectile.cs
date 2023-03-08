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
        float prevDuration = duration;
        duration -= Time.deltaTime;
        if (prevDuration > 0 && duration <= 0)
        {
            alreadyHit.Clear();
        }

        // homing
        if (duration <= 0)
        {
            direction.right = origin.transform.position - transform.position;
        }

        // movement
        transform.position += direction.right * (stats[StatType.MoveSpeed].Value + (Mathf.Abs(duration)*10)) * Time.deltaTime;

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
        //neat bitshift trick to compare layermask with layer
        if (collision.GetComponent<Entity>() != null && (1 << collision.gameObject.layer & origin.whatIsEnemy) != 0 && !alreadyHit.Contains(collision.GetComponent<Entity>()))
        {
            Vector2 knockback = new Vector2(100 * (duration <= 0 ? 1 : -1), 100);
            DamageScript.Attack(new DamageScript.attackData(origin, new DamageScript.damageData(origin.stats[StatType.AttackDamage].Value, DamageScript.damageType.physical), true, knockback, .5f), collision.GetComponent<Entity>(), origin);
            alreadyHit.Add(collision.GetComponent<Entity>());
        }

    }
}
