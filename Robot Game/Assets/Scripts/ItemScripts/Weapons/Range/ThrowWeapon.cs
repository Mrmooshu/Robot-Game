using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowWeapon : RangeWeapon
{
    public override void BasicAttack(MinionEntity player)
    {
        Projectile projectile = Instantiate(projectilePrefab, (Vector2)(player.transform.position) + new Vector2(firePosition.x * player.facingDirection, firePosition.y), player.transform.rotation).GetComponent<Projectile>();
        //projectile.Initialize(player.facingDirection, player);
        //testing effects
        //Effect.AddEffect(Database.GetEffectID("speed boost"), player);
        //Effect.AddEffect(Database.GetEffectID("jump drop"), player);
    }
}
