using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon/Melee/Fist", fileName = "Fist Weapon Item")]
public class FistWeapon : MeleeWeapon
{
    [Header("Fist Properties")]
    public HitColliderInfo[] FirstHitcolliders;
    public HitColliderInfo[] SecondHitcolliders;
}
