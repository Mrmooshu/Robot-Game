using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerEntity player;

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerEntity>();
    }

    public void CallBasicAttack()
    {
        player.BasicAttack();
    }

    public virtual void OnDrawGizmos()
    {
        if (player.core.currentBody.weapon != null)
        {
            Weapon weapon = (Weapon)Database.GetItem(player.core.currentBody.weapon.itemID);
            if (weapon is WarhammerWeapon)
            {
                WarhammerWeapon melee = (WarhammerWeapon)weapon;
                foreach (Weapon.HitColliderInfo collider in melee.Hitcolliders)
                {
                    Gizmos.DrawWireSphere(new Vector2((transform.position.x + collider.position.x * player.facingDirection), (transform.position.y + collider.position.y)), collider.radius);
                }
            }
        }
    }
}
