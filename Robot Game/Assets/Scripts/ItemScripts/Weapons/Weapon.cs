using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ItemData
{
    public List<HitColliderInfo> drawNow;

    [Header("Weapon Properties")]
    public RuntimeAnimatorController animController;
    public RuntimeAnimatorController animControllerBack = null;
    public int baseAttackSpeed;
    public int baseDamage;

    [System.Serializable]
    public struct HitColliderInfo
    {
        public Vector2 position;
        public float radius;
    }

    public abstract void BasicAttack(PlayerEntity player, int followUpIndex);

    
}
