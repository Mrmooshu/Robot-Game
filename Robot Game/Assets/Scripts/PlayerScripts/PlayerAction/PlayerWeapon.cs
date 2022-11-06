using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerEntity player;

    public Animator weaponAnimator { get; protected set; }

    public string weaponString { get; protected set; }

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerEntity>();
        weaponAnimator = GetComponent<Animator>();
        UpdateAnimators();
    }

    public void CallBasicAttack(AnimationEvent animEvent)
    {
        if (player.core.currentBody.weapon != null)
        {
            Weapon weapon = (Weapon)Database.GetItem(player.core.currentBody.weapon.itemID);
            weapon.BasicAttack(player, animEvent.intParameter);
        }
    }

    public void UpdateAnimators()
    {
        Weapon weapon = (Weapon)Database.GetItem(player.core.currentBody.weapon.itemID);

        if (name == "WeaponFront")
        {
            if (weapon.animController != null)
            {
                weaponAnimator.runtimeAnimatorController = weapon.animController;
            }
            else
            {
                weaponAnimator.runtimeAnimatorController = null;
            }
        }
        else if (name == "WeaponBack")
        {
            if (weapon.animControllerBack != null)
            {
                weaponAnimator.runtimeAnimatorController = weapon.animControllerBack;
            }
            else
            {
                weaponAnimator.runtimeAnimatorController = null;
            }
        }

        // weapon string
        if (weapon is CrushWeapon)
        {
            weaponString = "Crush";
        }
        else if (weapon is FistWeapon)
        {
            weaponString = "Punch";
        }
        else if (weapon is ThrowWeapon)
        {
            weaponString = "Throw";
        }
    }


    public virtual void OnDrawGizmos()
    {
        if (player.core.currentBody.weapon != null)
        {
            Weapon weapon = (Weapon)Database.GetItem(player.core.currentBody.weapon.itemID);
            foreach (Weapon.HitColliderInfo collider in weapon.drawNow)
            {
                Gizmos.DrawWireSphere(new Vector2((transform.position.x + collider.position.x * player.facingDirection), (transform.position.y + collider.position.y)), collider.radius);
            }
        }
    }
}
