using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon/Melee/Fist", fileName = "Fist Weapon Item")]
public class FistWeapon : MeleeWeapon
{
    [Header("Fist Properties")]
    public HitColliderInfo[] HitColliders;

    public override void BasicAttack(PlayerEntity player, int followUpIndex)
    {
        drawNow.Clear();
        HitColliderInfo collider = HitColliders[followUpIndex];
        Physics2D.OverlapCircleAll(new Vector2((player.transform.position.x + collider.position.x * player.facingDirection), (player.transform.position.y + collider.position.y)), collider.radius, player.whatIsEnemy);
        drawNow.Add(collider);
        // damage targets
    }
}
