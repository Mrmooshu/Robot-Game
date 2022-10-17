using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon/Melee/Warhammer", fileName = "Warhammer Weapon Item")]
public class WarhammerWeapon : MeleeWeapon
{
    [Header("Warhammer Properties")]
    public HitColliderInfo[] Hitcolliders;
}
