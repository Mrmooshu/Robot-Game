using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowWeapon : RangeWeapon
{
    public override void BasicAttack(PlayerEntity player, int followUpIndex)
    {
        Projectile projectile = Instantiate(projectilePrefab, (Vector2)(player.transform.position) + new Vector2(firePosition.x * player.facingDirection, firePosition.y), player.transform.rotation).GetComponent<Projectile>();
        projectile.Initialize(projectileSpeed, player.facingDirection, player);
        Effect buff = new Effect(Database.GetBuff("speed boost"), player);
    }
}
