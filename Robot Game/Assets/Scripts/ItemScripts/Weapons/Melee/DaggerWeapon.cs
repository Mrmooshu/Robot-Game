using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon/Melee/Dagger", fileName = "Dagger Weapon Item")]
public class DaggerWeapon : MeleeWeapon
{
    public override void BasicAttack(MinionEntity player)
    {
        Vector2 knockback = new Vector2(100 * player.facingDirection, 100);
        DamageScript.Attack(new DamageScript.attackData(player, new DamageScript.damageData(player.stats[EntityStatType.AttackDamage].Value, DamageScript.damageType.physical), true, knockback, .5f), player.hitboxes, player.whatIsEnemy, player);
    }
}
