using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageScript;

public class BoomerangProjectile : Projectile
{
    public float duration = 1;
    public float rotationSpeed = 450f;

    public override void Start()
    {
        base.Start();
        Vector2 knockback = new Vector2(100 * (duration <= 0 ? 1 : -1), 100);
        hitboxes.EnableAttack(new AttackData(origin, new damageData(origin.stats[EntityStatType.AttackDamage].Value, damageType.physical), true, (knockback, knockback), .5f, whatIsEnemy));
    }

    public override void Update()
    {
        base.Update();
        float prevDuration = duration;
        duration -= Time.deltaTime;
        if (prevDuration > 0 && duration <= 0)
        {
            hitboxes.attack.Totalhits = new List<Entity>();
        }

        // homing
        if (duration <= 0)
        {
            direction.right = origin.transform.position - transform.position;
        }

        // movement
        transform.position += direction.right * (stats[EntityStatType.MoveSpeed].Value + (Mathf.Abs(duration)*10)) * Time.deltaTime;

        // rotate boomerang
        visual.Rotate(0,0,-rotationSpeed * Time.deltaTime);

        // despawn if far enough away from origin
        if (Vector2.Distance(origin.transform.position, transform.position) > 100 || Vector2.Distance(origin.transform.position, transform.position) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
