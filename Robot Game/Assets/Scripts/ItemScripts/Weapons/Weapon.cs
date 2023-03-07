using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ItemData
{
    [Header("Weapon Properties")]
    public AnimationClip animation;
    public float baseAttackSpeed;
    public int baseDamage;

    public abstract void BasicAttack(PlayerEntity player);

    
}
