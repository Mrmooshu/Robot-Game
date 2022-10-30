using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon/Melee/Warhammer", fileName = "Warhammer Weapon Item")]
public class WarhammerWeapon : MeleeWeapon
{
    [Header("Warhammer Properties")]
    public HitColliderInfo[] Hitcolliders;

    public override void BasicAttack(PlayerEntity player, int followUpIndex)
    {
        drawNow.Clear();
        List<Collider2D> TargetsHit = new List<Collider2D>();
        foreach (HitColliderInfo collider in Hitcolliders)
        {
            drawNow.Add(collider);
            Collider2D[] hit = Physics2D.OverlapCircleAll(new Vector2((player.transform.position.x + collider.position.x * player.facingDirection), (player.transform.position.y + collider.position.y)), collider.radius, player.whatIsEnemy);
            foreach (Collider2D target in hit)
            {
                if (!TargetsHit.Contains(target))
                {
                    TargetsHit.Add(target);
                }
            }
        }
        // damage targets
    }
}
