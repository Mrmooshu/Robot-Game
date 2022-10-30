using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon/Range/Throw/Dart", fileName = "Dart Weapon Item")]
public class DartWeapon : ThrowWeapon
{
    public override void BasicAttack(PlayerEntity player, int followUpIndex)
    {
        Projectile projectile = Instantiate(projectilePrefab, (Vector2)(player.transform.position) + (firePosition*player.facingDirection), player.transform.rotation).GetComponent<Projectile>();
        projectile.Initialize(new Vector2(player.facingDirection,0), projectileSpeed);
    }
}
